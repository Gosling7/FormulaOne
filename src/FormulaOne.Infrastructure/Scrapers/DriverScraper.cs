using FormulaOne.Core.Entities;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace FormulaOne.Infrastructure.Scrapers;

internal class DriverScraper : ScraperBase
{
    private const string TableRowPath = "//table[@class='f1-table f1-table-with-data w-full']/tbody/tr";
    private const string NameParagraphPath = "./td[@class='p-normal whitespace-nowrap']/p";

    internal async Task<ConcurrentDictionary<string, Driver>> ScrapeAsync(
        int startYear, int endYear, IEnumerable<string> raceResultsLinks)
    {
        var stopwatch = Stopwatch.StartNew();
        var drivers = await ScrapeFromRaceResults(raceResultsLinks);
        await AddNationalitiesToDrivers(drivers, startYear, endYear);
        stopwatch.Stop();

        Console.WriteLine($"Drivers scraped. Elapsed: {stopwatch.Elapsed}.");

        return drivers;
    }

    internal async Task<ConcurrentDictionary<string, Driver>> ScrapeFromRaceResults(
        IEnumerable<string> raceResultsLinks)
    {
        var drivers = new ConcurrentDictionary<string, Driver>();

        var tasks = raceResultsLinks.Select(async link =>
        {
            var htmlDocument = await base._htmlWeb.LoadFromWebAsync(link);
            var rows = htmlDocument.DocumentNode.SelectNodes(TableRowPath) ?? null;
            if (rows == null)
            {
                return;
            }

            foreach (var row in rows)
            {
                var scrapedFirstName = row.SelectSingleNode(
                    NameParagraphPath + "/span[1]").InnerText.Trim();
                var scrapedLastName = row.SelectSingleNode(
                    NameParagraphPath + "/span[2]").InnerText.Trim();
                drivers.TryAdd($"{scrapedFirstName} {scrapedLastName}", new Driver(Guid.NewGuid(),
                    scrapedFirstName, scrapedLastName, null));
            }
        });

        await Task.WhenAll(tasks);

        return drivers;
    }

    internal async Task AddNationalitiesToDrivers(
        ConcurrentDictionary<string, Driver> driversWithoutNationalities,
        int startYear, int endYear)
    {
        var tasks = Enumerable.Range(startYear, endYear - startYear)
            .Select(async year =>
            {
                var htmlDocument = await base._htmlWeb.LoadFromWebAsync(
                    $"https://www.formula1.com/en/results.html/{year}/drivers.html");
                foreach (var row in htmlDocument.DocumentNode.SelectNodes(TableRowPath))
                {
                    string scrapedFirstName = row.SelectSingleNode(
                        "./td/p/a/span[1]")?.InnerText.Trim() ?? string.Empty;
                    // If there's no first name on the page - a season hasn't started yet.
                    if (string.IsNullOrWhiteSpace(scrapedFirstName))
                    {
                        break;
                    }

                    var scrapedLastName = row.SelectSingleNode(
                        "./td/p/a/span[2]").InnerText.Trim();
                    if (!driversWithoutNationalities.TryGetValue($"{scrapedFirstName} {scrapedLastName}",
                        out var existingDriver))
                    {
                        continue;
                    }

                    if (existingDriver.Nationality is null)
                    {
                        string scrapedNationality = row.SelectSingleNode("./td[3]/p").InnerText.Trim();
                        existingDriver.Nationality = scrapedNationality;
                    }
                }
            });

        await Task.WhenAll(tasks);

        return;
    }
}

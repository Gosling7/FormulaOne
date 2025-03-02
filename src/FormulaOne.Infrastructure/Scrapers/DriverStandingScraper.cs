using FormulaOne.Core.Entities;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace FormulaOne.Infrastructure.Scrapers;

internal class DriverStandingScraper : ScraperBase
{
    internal async Task<ConcurrentBag<DriverStanding>> ScrapeAsync(
        int startYear, int endYear,
        ConcurrentDictionary<string, Driver> drivers,
        ConcurrentDictionary<string, Team> teams)
    {
        var driverStandings = new ConcurrentBag<DriverStanding>();

        var stopwatch = Stopwatch.StartNew();
        var tasks = Enumerable.Range(startYear, endYear - startYear)
            .Select(async year =>
            {
                var htmlDocument = await base._htmlWeb.LoadFromWebAsync(
                    $"https://www.formula1.com/en/results.html/{year}/drivers.html");
                foreach (var row in htmlDocument.DocumentNode.SelectNodes(
                    "//table[@class='f1-table f1-table-with-data w-full']/tbody/tr"))
                {
                    var scrapedFirstName = row.SelectSingleNode(
                        "./td[2]/p/a/span[1]")?.InnerText.Trim();
                    var scrapedLastName = row.SelectSingleNode(
                        "./td[2]/p/a/span[@class='max-tablet:hidden']")?.InnerText.Trim();
                    if (!drivers.TryGetValue($"{scrapedFirstName} {scrapedLastName}",
                        out var existingDriver))
                    {
                        continue;
                    }

                    var scrapedTeamName = row.SelectSingleNode("./td[4]/p/a")?.InnerText.Trim()
                        ?? string.Empty;
                    if (!teams.TryGetValue(scrapedTeamName, out var existingTeam))
                    {
                        continue;
                    }

                    var scrapedPositionString = row.SelectSingleNode("./td[1]/p")?.InnerText;
                    if (!int.TryParse(scrapedPositionString, out int position))
                    {
                        position = 0;
                    }

                    var scrapedPointsString = row.SelectSingleNode("./td[5]/p")?.InnerText;
                    if (!float.TryParse(scrapedPointsString, out float points))
                    {
                        points = 0;
                    }

                    driverStandings.Add(new DriverStanding(
                        id: Guid.NewGuid(),
                        position: position,
                        driver: existingDriver,
                        team: existingTeam,
                        points: points,
                        year: year));
                }
            });

        await Task.WhenAll(tasks);
        stopwatch.Stop();

        Console.WriteLine($"DriverStandings scraped. Elapsed: {stopwatch.Elapsed}.");

        return driverStandings;
    }
}

using FormulaOne.Core.Entities;
using HtmlAgilityPack;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace FormulaOne.Infrastructure.Scrapers;

internal class RaceResultScraper : ScraperBase
{
    protected const string DateAndNameDivPath = "//div[@class='max-tablet:flex-col flex gap-xs']";

    internal async Task<ConcurrentBag<RaceResult>> ScrapeAsync(
        ConcurrentBag<string> links,
        ConcurrentDictionary<string, Circuit> circuits,
        ConcurrentDictionary<string, Driver> drivers,
        ConcurrentDictionary<string, Team> teams)
    {
        var raceResults = new ConcurrentBag<RaceResult>();

        var stopwatch = Stopwatch.StartNew();
        var tasks = links.Select(async link =>
        {
            var htmlDocument = await base._htmlWeb.LoadFromWebAsync(link);
            var rows = htmlDocument.DocumentNode.SelectNodes(
                "//table[@class='f1-table f1-table-with-data w-full']/tbody/tr");
            if (rows is null)
            {
                return;
            }

            var circuitFullName = htmlDocument.DocumentNode.SelectSingleNode(
                DateAndNameDivPath + "/p[2]").InnerText;
            var scrapedParts = circuitFullName.Split(',');
            if (!circuits.TryGetValue(scrapedParts[0], out var existingCircuit))
            {
                Console.WriteLine($"Circuit: {circuitFullName} not found in Circuits table!");
            }

            var scrapedDateString = htmlDocument.DocumentNode.SelectSingleNode(
                DateAndNameDivPath + "/p[1]").InnerText;
            var dateParts = scrapedDateString.Split(" - ");
            var dateString = dateParts.Length > 1
                ? dateParts[1]
                : scrapedDateString;
            if (!DateOnly.TryParseExact(dateString, "dd MMM yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly date))
            {
                Console.WriteLine($"Something's wrong: {link} \n" +
                    $"Date: {scrapedDateString}");
            }

            foreach (var row in rows)
            {
                var scrapedPositionString = row.SelectSingleNode("./td[1]/p").InnerText;
                if (!int.TryParse(scrapedPositionString, out int position))
                {
                    position = 0;
                }

                var scrapedPointsString = row.SelectSingleNode("./td[7]/p").InnerText;
                if (!float.TryParse(scrapedPointsString, CultureInfo.InvariantCulture,
                    out float points))
                {
                    points = 0;
                }

                var driverFirstName = row.SelectSingleNode("./td[3]/p/span[1]").InnerText.Trim();
                var driverLastName = row.SelectSingleNode("./td[3]/p/span[2]").InnerText.Trim();
                if (!drivers.TryGetValue($"{driverFirstName} {driverLastName}",
                    out var existingDriver))
                {
                    Console.WriteLine(
                        $"Driver: {driverFirstName} {driverLastName} not found in Drivers table!");
                }

                var teamName = row.SelectSingleNode("./td[4]/p").InnerText.Trim();
                if (!teams.TryGetValue(teamName, out var existingTeam))
                {
                    Console.WriteLine(
                        $"Team: {teamName} not found in Teams table!");
                }

                var scrapedLapsString = row.SelectSingleNode("./td[5]/p").InnerText.Trim();
                if (!int.TryParse(scrapedLapsString, out int laps))
                {
                    laps = 0;
                }

                var timeString = row.SelectSingleNode("./td[6]/p").InnerText.Trim();
                raceResults.Add(new RaceResult
                    (id: Guid.NewGuid(),
                    position: position,
                    circuit: existingCircuit,
                    date: date,
                    driver: existingDriver,
                    team: existingTeam,
                    laps: laps,
                    time: timeString,
                    points: points));
            }
        });

        await Task.WhenAll(tasks);
        stopwatch.Stop();

        Console.WriteLine($"RaceResults scraped. Elapsed: {stopwatch.Elapsed}.");

        return raceResults;
    }
}

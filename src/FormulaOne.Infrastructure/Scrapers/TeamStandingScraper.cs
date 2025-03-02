using FormulaOne.Core.Entities;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Globalization;

namespace FormulaOne.Infrastructure.Scrapers;

internal class TeamStandingScraper : ScraperBase
{
    internal async Task<ConcurrentBag<TeamStanding>> ScrapeAsync(
        int startYear, int endYear, ConcurrentDictionary<string, Team> teams)
    {
        var teamStandings = new ConcurrentBag<TeamStanding>();

        var stopwatch = Stopwatch.StartNew();
        var tasks = Enumerable.Range(startYear, endYear - startYear)
            .Select(async year =>
            {
                var htmlDocument = await base._htmlWeb.LoadFromWebAsync(
                    $"https://www.formula1.com/en/results.html/{year}/team.html");
                foreach (var row in htmlDocument.DocumentNode.SelectNodes(
                    "//table[@class='f1-table f1-table-with-data w-full']/tbody/tr"))
                {
                    var scrapedTeamName = row.SelectSingleNode(
                        "./td[2]")?.InnerText.Trim() ?? string.Empty;
                    if (!teams.TryGetValue(scrapedTeamName, out var existingTeam))
                    {
                        Console.WriteLine($"Team: {scrapedTeamName} not found in Teams table!");
                        continue;
                    }

                    var scrapedPositionString = row.SelectSingleNode("./td[1]").InnerText;
                    if (!int.TryParse(scrapedPositionString, out int position))
                    {
                        position = 0;
                    }

                    var scrapedPointsString = row.SelectSingleNode("./td[3]").InnerText;
                    if (!float.TryParse(scrapedPointsString, CultureInfo.InvariantCulture,
                        out float points))
                    {
                        points = 0;
                    }

                    teamStandings.Add(new TeamStanding(
                        id: Guid.NewGuid(),
                        year: year,
                        position: position,
                        points: points,
                        team: existingTeam));
                }
            });

        await Task.WhenAll(tasks);
        stopwatch.Stop();

        Console.WriteLine($"TeamStandings scraped. Elapsed: {stopwatch.Elapsed}.");

        return teamStandings;
    }
}

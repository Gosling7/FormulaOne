using FormulaOne.Core.Entities;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace FormulaOne.Infrastructure.Scrapers;

internal class TeamScraper : ScraperBase
{
    internal async Task<ConcurrentDictionary<string, Team>> ScrapeAsync(IEnumerable<string> links)
    {
        var teams = new ConcurrentDictionary<string, Team>();

        var stopwatch = Stopwatch.StartNew();
        var tasks = links.Select(async link =>
        {
            var htmlDocument = await base._htmlWeb.LoadFromWebAsync(link);
            var rows = htmlDocument.DocumentNode.SelectNodes(
                "//table[@class='f1-table f1-table-with-data w-full']/tbody/tr") ?? null;
            // If there's no row node on the page - the race hasn't taken place yet.
            if (rows == null)
            {
                return;
            }

            foreach (var row in rows)
            {
                var scrapedName = row.SelectSingleNode("./td[4]/p")?.InnerText.Trim();
                if (!string.IsNullOrEmpty(scrapedName))
                {
                    teams.TryAdd(scrapedName, new Team(Guid.NewGuid(), scrapedName ?? string.Empty));
                }
            }
        });

        await Task.WhenAll(tasks);
        stopwatch.Stop();

        Console.WriteLine($"Teams scraped. Elapsed: {stopwatch.Elapsed}.");

        return teams;
    }
}

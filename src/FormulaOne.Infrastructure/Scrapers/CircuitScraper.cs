using FormulaOne.Core.Entities;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace FormulaOne.Infrastructure.Scrapers;

internal class CircuitScraper : ScraperBase
{
    internal async Task<ConcurrentDictionary<string, Circuit>> ScrapeAsync(
        IEnumerable<string> raceResultsLinks)
    {
        var circuits = new ConcurrentDictionary<string, Circuit>();

        var stopwatch = Stopwatch.StartNew();
        var tasks = raceResultsLinks.Select(async link =>
        {
            var htmlDocument = await base._htmlWeb.LoadFromWebAsync(link);
            var scrapedFullName = htmlDocument.DocumentNode.SelectSingleNode(
                "//div[@class='max-tablet:flex-col flex gap-xs']/p[2]")?.InnerText;
            if (string.IsNullOrWhiteSpace(scrapedFullName))
            {
                return;
            }

            var scrapedParts = scrapedFullName.Split(',');
            var scrapedName = scrapedParts[0].Trim();
            var scrapedLocation = scrapedParts[1].Trim();
            circuits.TryAdd(scrapedName, new Circuit(
                id: Guid.NewGuid(),
                name: scrapedName,
                location: scrapedLocation));
        });

        await Task.WhenAll(tasks);
        stopwatch.Stop();

        Console.WriteLine($"Circuits scraped. Elapsed: {stopwatch.Elapsed}.");

        return circuits;
    }
}


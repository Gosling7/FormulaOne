using System.Collections.Concurrent;
using System.Diagnostics;

namespace FormulaOne.Infrastructure.Scrapers;

internal class RaceResultLinksScraper : ScraperBase
{
    internal async Task<ConcurrentBag<string>> ScrapeAsync(int startYear, int endYear)
    {
        var links = new ConcurrentBag<string>();

        var stopwatch = Stopwatch.StartNew();
        var tasks = Enumerable.Range(startYear, endYear - startYear)
            .Select(async year =>
            {
                var htmlDocument = await base._htmlWeb.LoadFromWebAsync(
                    $"https://www.formula1.com/en/results.html/{year}/races.html");
                var htmlTableRows = htmlDocument.DocumentNode.SelectNodes(
                    "//table[@class='f1-table f1-table-with-data w-full']" +
                    "/tbody" +
                    "/tr");
                foreach (var row in htmlTableRows)
                {
                    var link = row.SelectSingleNode("./td/p/a").Attributes["href"].Value;
                    if (!link.EndsWith("races.html"))
                    {
                        var fullLink = $"https://www.formula1.com/en/results/{year}/{link}";

                        links.Add(fullLink);
                    }
                }
            });

        await Task.WhenAll(tasks);
        stopwatch.Stop();

        Console.WriteLine($"Race result links scraped. Elapsed: {stopwatch.Elapsed}.");

        return links;
    }
}

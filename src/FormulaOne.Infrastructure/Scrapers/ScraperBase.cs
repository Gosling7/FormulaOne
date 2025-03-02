using HtmlAgilityPack;
using System.Text;

namespace FormulaOne.Infrastructure.Scrapers;

internal class ScraperBase
{
    protected readonly HtmlWeb _htmlWeb;

    public ScraperBase()
    {
        _htmlWeb = new HtmlWeb
        {
            OverrideEncoding = Encoding.UTF8
        };
    }
}

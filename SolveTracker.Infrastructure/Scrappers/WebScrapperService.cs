using PuppeteerSharp;
using SolveTracker.Domain.Scrappers;

namespace SolveTracker.Infrastructure.Scrappers;

public class WebScrapperService : IWebScrapperService
{
    public async Task<string> GetDynamicHtmlContentAsync(string url)
    {
        BrowserFetcher browserFetcher = new();
        _ = await browserFetcher.DownloadAsync();
        using IBrowser browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
        using IPage page = await browser.NewPageAsync();
        _ = await page.GoToAsync(url);
        string content = await page.GetContentAsync();
        return content;
    }
}

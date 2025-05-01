using PuppeteerSharp;

namespace SolveTracker.Infrastructure.Scrappers;

internal class WebScrapperService
{
    public static async Task<string> GetDynamicHtmlContentAsync(string url)
    {
        var browserFetcher = new BrowserFetcher();
        _ = await browserFetcher.DownloadAsync();
        using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
        using var page = await browser.NewPageAsync();
        await page.GoToAsync(url);
        var content = await page.GetContentAsync();
        return content;
    }
}

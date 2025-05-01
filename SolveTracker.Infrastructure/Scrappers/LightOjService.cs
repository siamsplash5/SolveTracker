using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using SolveTracker.Domain.Scrappers;

namespace SolveTracker.Infrastructure.Scrappers;

public class LightOjService(IWebScrapperService webScrapperService, ILogger<LightOjService> logger) : ILightOjService
{
    private readonly string _baseUrl = "https://lightoj.com/user/";

    public async Task<int> GetSolveCountByScrappingAsync(string username)
    {
        try
        {
            logger.LogInformation("LightOj scrapping has started...");

            string url = $"{_baseUrl}{username}";
            var htmlContent = await webScrapperService.GetDynamicHtmlContentAsync(url);
            var htmlDocument = new HtmlDocument();

            htmlDocument.LoadHtml(htmlContent);

            string xPath = "//*[@id=\"pages-community\"]/div[2]/div[2]/div/div[2]/div[1]/div[1]/span[1]";
            string innerText = htmlDocument.DocumentNode.SelectSingleNode(xPath)?.InnerText ?? string.Empty;
            string countText = innerText.Trim();
            _ = int.TryParse(countText, out int count);

            logger.LogInformation("LightOj scrapping has finished.");

            return count;
        }
        catch (Exception)
        {
            throw;
        }
    }
}

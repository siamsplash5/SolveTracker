using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using SolveTracker.Domain.Scrappers;

namespace SolveTracker.Infrastructure.Scrappers;

public class CsesService(IWebScrapperService webScrapperService, ILogger<CsesService> logger) : ICsesService
{
    private readonly string _baseUrl = "https://cses.fi/user/";

    public async Task<int> GetSolveCountByScrappingAsync(string userId)
    {
        try
        {
            logger.LogInformation("Cses scrapping has started...");

            string url = $"{_baseUrl}{userId}";
            string htmlContent = await webScrapperService.GetDynamicHtmlContentAsync(url);
            HtmlDocument htmlDocument = new();

            htmlDocument.LoadHtml(htmlContent);

            string xPath = "/html/body/div[2]/div[2]/div/table[2]/tbody/tr/td[2]";
            string innerText = htmlDocument.DocumentNode.SelectSingleNode(xPath)?.InnerText ?? string.Empty;
            string countText = innerText.Trim();
            _ = int.TryParse(countText, out int count);

            logger.LogInformation("Cses scrapping has finished.");

            return count;
        }
        catch (Exception)
        {
            throw;
        }
    }
}

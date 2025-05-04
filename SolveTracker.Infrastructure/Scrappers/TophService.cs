using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using SolveTracker.Domain.Scrappers;

namespace SolveTracker.Infrastructure.Scrappers;

public class TophService(IWebScrapperService webScrapperService, ILogger<TophService> logger) : ITophService
{
    private readonly string _baseUrl = "https://toph.co/u/";

    public async Task<int> GetSolveCountByScrappingAsync(string username)
    {
        try
        {
            logger.LogInformation("Toph scrapping has started...");

            string url = $"{_baseUrl}{username}";
            string htmlContent = await webScrapperService.GetDynamicHtmlContentAsync(url);
            HtmlDocument htmlDocument = new();

            htmlDocument.LoadHtml(htmlContent);

            string xPath = "/html/body/div[1]/div/div/div[1]/div/div[2]/div[1]/div/div[2]/div[1]";
            string innerText = htmlDocument.DocumentNode.SelectSingleNode(xPath)?.InnerText ?? string.Empty;
            string countText = innerText.Trim();
            _ = int.TryParse(countText, out int count);

            logger.LogInformation("Toph scrapping has finished.");

            return count;
        }
        catch (Exception)
        {
            throw;
        }
    }
}

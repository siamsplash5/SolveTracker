using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using SolveTracker.Domain.Scrappers;

namespace SolveTracker.Infrastructure.Scrappers;

public class SpojService(IWebScrapperService webScrapperService, ILogger<SpojService> logger) : ISpojService
{
    private readonly string _baseUrl = "https://www.spoj.com/users/";

    public async Task<int> GetSolveCountByScrappingAsync(string username)
    {
        try
        {
            logger.LogInformation("Spoj scrapping has started...");

            string url = $"{_baseUrl}{username}";
            string htmlContent = await webScrapperService.GetDynamicHtmlContentAsync(url);
            HtmlDocument htmlDocument = new();

            htmlDocument.LoadHtml(htmlContent);

            string xPath = "//*[@id=\"content\"]/div[2]/div/div[2]/div[1]/div/div[2]/div[1]/dl/dd[1]/text()";
            string innerText = htmlDocument.DocumentNode.SelectSingleNode(xPath)?.InnerText ?? string.Empty;
            string countText = innerText.Trim();
            _ = int.TryParse(countText, out int count);

            logger.LogInformation("Spoj scrapping has finished.");

            return count;
        }
        catch (Exception)
        {
            throw;
        }
    }
}

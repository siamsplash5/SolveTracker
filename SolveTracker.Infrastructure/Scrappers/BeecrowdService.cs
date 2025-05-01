using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using SolveTracker.Domain.Scrappers;

namespace SolveTracker.Infrastructure.Scrappers;

public class BeecrowdService(IWebScrapperService webScrapperService, ILogger<BeecrowdService> logger) : IBeecrowdService
{
    private const string _baseUrl = "https://www.beecrowd.com.br/judge/en/profile/";

    public async Task<int> GetSolveCountByScrappingAsync(string userId)
    {
        try
        {
            logger.LogInformation("Beecrowd scrapping has started...");

            string url = $"{_baseUrl}{userId}";
            var htmlContent = await webScrapperService.GetDynamicHtmlContentAsync(url);
            var htmlDocument = new HtmlDocument();

            htmlDocument.LoadHtml(htmlContent);

            string xPath = "//*[@id=\"profile-bar\"]/ul/li[6]";
            string innerText = htmlDocument.DocumentNode.SelectSingleNode(xPath)?.InnerText ?? string.Empty;
            string countText = innerText.Trim();
            _ = int.TryParse(countText, out int count);

            logger.LogInformation("Beecrowd scrapping has finished.");

            return count;
        }
        catch (Exception)
        {
            throw;
        }
    }
}

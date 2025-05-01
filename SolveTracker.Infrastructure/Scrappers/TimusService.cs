using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using SolveTracker.Domain.Scrappers;

namespace SolveTracker.Infrastructure.Scrappers;

public class TimusService(IWebScrapperService webScrapperService, ILogger<TimusService> logger) : ITimusService
{
    private readonly string _baseUrl = "https://acm.timus.ru/author.aspx?id=";

    public async Task<int> GetSolveCountByScrappingAsync(string judgeId)
    {
        try
        {
            logger.LogInformation("Timus scrapping has started...");

            string url = $"{_baseUrl}{judgeId}";
            var htmlContent = await webScrapperService.GetDynamicHtmlContentAsync(url);
            var htmlDocument = new HtmlDocument();

            htmlDocument.LoadHtml(htmlContent);

            string xPath = "/html/body/table/tbody/tr[3]/td/table/tbody/tr/td/table[1]/tbody/tr[2]/td[2]";
            string innerText = htmlDocument.DocumentNode.SelectSingleNode(xPath)?.InnerText ?? string.Empty;
            string countText = innerText.Split(" ").First().Trim();
            _ = int.TryParse(countText, out int count);

            logger.LogInformation("Timus scrapping has finished.");

            return count;
        }
        catch (Exception)
        {
            throw;
        }
    }
}

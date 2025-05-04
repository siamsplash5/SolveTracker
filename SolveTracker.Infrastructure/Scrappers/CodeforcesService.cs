using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using SolveTracker.Domain.Scrappers;
using System.Text.RegularExpressions;

namespace SolveTracker.Infrastructure.Scrappers;

public partial class CodeforcesService(IWebScrapperService webScrapperService, ILogger<CodeforcesService> logger) : ICodeforcesService
{
    private readonly string _baseUrl = "https://codeforces.com/profile/";

    public async Task<int> GetSolveCountByScrappingAsync(string username)
    {
        try
        {
            logger.LogInformation("Codeforces scrapping has started...");

            string url = $"{_baseUrl}{username}";
            string htmlContent = await webScrapperService.GetDynamicHtmlContentAsync(url);
            HtmlDocument htmlDocument = new();

            htmlDocument.LoadHtml(htmlContent);

            string xPath = "//*[@id=\"pageContent\"]/div[4]/div/div[3]/div[1]/div[1]/div[1]";
            string innerText = htmlDocument.DocumentNode.SelectSingleNode(xPath)?.InnerText ?? string.Empty;
            string countText = ExtractCountRegex().Replace(innerText, "").Trim();
            _ = int.TryParse(countText, out int count);

            logger.LogInformation("Codeforces scrapping has finished.");

            return count;
        }
        catch (Exception)
        {
            throw;
        }
    }

    [GeneratedRegex(@"\bproblems?\b")]
    private static partial Regex ExtractCountRegex();
}

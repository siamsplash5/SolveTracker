using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using SolveTracker.Domain.Scrappers;
using System.Text.RegularExpressions;

namespace SolveTracker.Infrastructure.Scrappers;

public partial class CodechefService(IWebScrapperService webScrapperService, ILogger<CodechefService> logger) : ICodechefService
{
    private readonly string _baseUrl = "https://www.codechef.com/users/";

    public async Task<int> GetSolveCountByScrappingAsync(string username)
    {
        try
        {
            logger.LogInformation("Codechef scrapping has started...");

            string url = $"{_baseUrl}{username}";
            string htmlContent = await webScrapperService.GetDynamicHtmlContentAsync(url);
            HtmlDocument htmlDocument = new();

            htmlDocument.LoadHtml(htmlContent);

            string xPath = "/html/body/main/div/div/div/div/div/section[4]/h3[4]";
            string innerText = htmlDocument.DocumentNode.SelectSingleNode(xPath)?.InnerText ?? string.Empty;
            string countText = ExtractCountRegex().Replace(innerText, "").Trim();
            _ = int.TryParse(countText, out int count);

            logger.LogInformation("Codechef scrapping has finished.");

            return count;
        }
        catch (Exception)
        {
            throw;
        }
    }

    [GeneratedRegex(@"\bTotal Problems? Solved: \b")]
    private static partial Regex ExtractCountRegex();
}

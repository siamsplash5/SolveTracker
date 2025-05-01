namespace SolveTracker.Domain.Scrappers;

public interface IWebScrapperService
{
    public Task<string> GetDynamicHtmlContentAsync(string url);
}

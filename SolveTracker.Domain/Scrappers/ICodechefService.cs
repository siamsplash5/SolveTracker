namespace SolveTracker.Domain.Scrappers;

public interface ICodechefService
{
    public Task<int> GetSolveCountByScrappingAsync(string username);
}

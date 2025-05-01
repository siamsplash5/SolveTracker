namespace SolveTracker.Domain.Scrappers;

public interface ITophService
{
    public Task<int> GetSolveCountByScrappingAsync(string username);
}

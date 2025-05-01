namespace SolveTracker.Domain.Scrappers;

public interface ISpojService
{
    public Task<int> GetSolveCountByScrappingAsync(string username);
}

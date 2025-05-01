namespace SolveTracker.Domain.Scrappers;

public interface ICodeforcesService
{
    public Task<int> GetSolveCountByScrappingAsync(string username);
}

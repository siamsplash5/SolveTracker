namespace SolveTracker.Domain.Scrappers;

public interface IBeecrowdService
{
    public Task<int> GetSolveCountByScrappingAsync(string userId);
}

namespace SolveTracker.Domain.Scrappers;

public interface ICsesService
{
    public Task<int> GetSolveCountByScrappingAsync(string userId);
}

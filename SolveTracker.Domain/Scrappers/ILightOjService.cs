namespace SolveTracker.Domain.Scrappers;

public interface ILightOjService
{
    public Task<int> GetSolveCountByScrappingAsync(string username);
}

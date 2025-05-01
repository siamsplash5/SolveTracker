namespace SolveTracker.Domain.Scrappers;

public interface ITimusService
{
    public Task<int> GetSolveCountByScrappingAsync(string judgeId);
}

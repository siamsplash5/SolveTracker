namespace SolveTracker.Domain.ApiServices;

public interface IUvaApiService
{
    public Task<int> GetSolveCountByAPIAsync(string userId);
}

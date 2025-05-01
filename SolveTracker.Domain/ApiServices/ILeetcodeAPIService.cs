namespace SolveTracker.Domain.ApiServices;

public interface ILeetcodeApiService
{
    public Task<int> GetSolveCountByAPIAsync(string username);
}

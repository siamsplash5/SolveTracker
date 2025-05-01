namespace SolveTracker.Domain.ApiServices;

public interface ICodeforcesApiService
{
    Task<int> GetSolveCountByAPIAsync(string username);
}

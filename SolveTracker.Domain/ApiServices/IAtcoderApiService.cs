namespace SolveTracker.Domain.ApiServices;

public interface IAtcoderApiService
{
    public Task<int> GetSolveCountByAPIAsync(string username);
}

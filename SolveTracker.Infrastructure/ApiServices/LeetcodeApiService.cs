using Microsoft.Extensions.Logging;
using SolveTracker.Domain.ApiServices;
using System.Text.Json;

namespace SolveTracker.Infrastructure.ApiServices;

public class LeetcodeApiService(ILogger<LeetcodeApiService> logger) : ILeetcodeApiService
{
    private const string _baseAPIUrl = "https://leetcode-stats-api.herokuapp.com/";

    public async Task<int> GetSolveCountByAPIAsync(string username)
    {
        try
        {
            logger.LogInformation("LeetCode API call has started...");

            string url = $"{_baseAPIUrl}{username}";
            using var httpClient = new HttpClient();
            var jsonResponse = await httpClient.GetStringAsync(url);
            var data = JsonSerializer.Deserialize<JsonElement>(jsonResponse);
            int count = data.GetProperty("totalSolved").GetInt32();

            logger.LogInformation("LeetCode API call has finished.");

            return count;
        }
        catch (Exception)
        {
            throw;
        }
    }
}

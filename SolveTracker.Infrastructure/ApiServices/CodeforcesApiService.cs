using Microsoft.Extensions.Logging;
using SolveTracker.Domain.ApiServices;
using System.Text.Json;

namespace SolveTracker.Infrastructure.ApiServices;

public class CodeforcesApiService(ILogger<CodeforcesApiService> logger) : ICodeforcesApiService
{
    private readonly string _baseAPIUrl = "https://codeforces.com/api/user.status?handle=";

    public async Task<int> GetSolveCountByAPIAsync(string username)
    {
        try
        {
            logger.LogInformation("Codeforces API call has started...");

            var url = $"{_baseAPIUrl}{username}";
            using var httpClient = new HttpClient();
            var jsonResponse = await httpClient.GetStringAsync(url);
            var response = JsonSerializer.Deserialize<JsonElement>(jsonResponse);
            var submissions = response.GetProperty("result").EnumerateArray().ToList();
            // Div 1 + Div 2 solve count (if same problem solved from 2 div)
            int count = submissions.Count(submission => submission.GetProperty("verdict").ToString() == "OK");

            logger.LogInformation("Codeforces API call has finished.");

            return count;
        }
        catch (Exception)
        {
            throw;
        }
    }
}

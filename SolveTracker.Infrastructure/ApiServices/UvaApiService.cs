using Microsoft.Extensions.Logging;
using SolveTracker.Domain.ApiServices;
using System.Text.Json;

namespace SolveTracker.Infrastructure.ApiServices;

public class UvaApiService(ILogger<UvaApiService> logger) : IUvaApiService
{
    private readonly string _baseAPIUrl = "https://uhunt.onlinejudge.org/api/subs-user/";

    public async Task<int> GetSolveCountByAPIAsync(string userId)
    {
        logger.LogInformation("UVa API call has started...");

        string url = $"{_baseAPIUrl}{userId}";
        using var httpClient = new HttpClient();
        var jsonResponse = await httpClient.GetStringAsync(url);
        var data = JsonSerializer.Deserialize<JsonElement>(jsonResponse);
        int[][] subs = JsonSerializer.Deserialize<int[][]>(data.GetProperty("subs").GetRawText());
        int acceptedVerdictId = 90;
        int count = subs.Where(submissionData => submissionData[2] == acceptedVerdictId)
                        .Select(submissionData => submissionData[1])
                        .Distinct()
                        .Count();

        logger.LogInformation("UVa API call has finished.");

        return count;
    }
}

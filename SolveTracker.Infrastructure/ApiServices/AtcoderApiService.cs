using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SolveTracker.Domain.ApiServices;
using SolveTracker.Models.Common;
using System.IO.Compression;

namespace SolveTracker.Infrastructure.ApiServices;

public class AtcoderApiService(ILogger<AtcoderApiService> logger): IAtcoderApiService
{
    private const string _baseAPIUrl = "https://kenkoooo.com/atcoder/atcoder-api/v3/user/ac_rank";

    public async Task<int> GetSolveCountByAPIAsync(string username)
    {
        try
        {
            logger.LogInformation("Atcoder API call has started...");

            using var httpClient = CreateHttpClient(_baseAPIUrl);
            string parameter = $"?user={username}";
            var response = await httpClient.GetAsync(parameter).ConfigureAwait(false);

            logger.LogInformation("Atcoder API call has finished.");

            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                using var decompressedStream = new GZipStream(contentStream, CompressionMode.Decompress);
                using var streamReader = new StreamReader(decompressedStream);
                var content = await streamReader.ReadToEndAsync();
                var apiResponse = JsonConvert.DeserializeObject<AtCoderApiResponse>(content);

                return apiResponse?.Count ?? 0;
            }

            return 0;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private static HttpClient CreateHttpClient(string baseUrl)
    {
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri(baseUrl)
        };

        httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36");
        httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br, zstd");
        httpClient.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9");
        httpClient.DefaultRequestHeaders.Add("Cache-Control", "max-age=0");
        return httpClient;
    }
}

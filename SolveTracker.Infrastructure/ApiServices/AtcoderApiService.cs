using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SolveTracker.Domain.ApiServices;
using SolveTracker.Domain.Entities.Common;
using System.IO.Compression;

namespace SolveTracker.Infrastructure.ApiServices;

public class AtcoderApiService(ILogger<AtcoderApiService> logger) : IAtcoderApiService
{
    private const string _baseAPIUrl = "https://kenkoooo.com/atcoder/atcoder-api/v3/user/ac_rank";

    public async Task<int> GetSolveCountByAPIAsync(string username)
    {
        try
        {
            logger.LogInformation("Atcoder API call has started...");

            using HttpClient httpClient = CreateHttpClient(_baseAPIUrl);
            string parameter = $"?user={username}";
            HttpResponseMessage response = await httpClient.GetAsync(parameter).ConfigureAwait(false);

            logger.LogInformation("Atcoder API call has finished.");

            if (response.IsSuccessStatusCode)
            {
                Stream contentStream = await response.Content.ReadAsStreamAsync();
                using GZipStream decompressedStream = new(contentStream, CompressionMode.Decompress);
                using StreamReader streamReader = new(decompressedStream);
                string content = await streamReader.ReadToEndAsync();
                AtCoderApiResponse apiResponse = JsonConvert.DeserializeObject<AtCoderApiResponse>(content);

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
        HttpClient httpClient = new()
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

using SolveTracker.Domain.ApiServices;
using SolveTracker.Domain.Entities.Dashboard;
using SolveTracker.Domain.Repositories;
using SolveTracker.Domain.Scrappers;
using System.Diagnostics;

namespace SolveTracker.Application.Services.ScrapperWorker;

public class ScrapperWorkerService(
    IAtcoderApiService atCoderService,
    ICodechefService codechefService,
    ICodeforcesService codeforcesService,
    ILeetcodeApiService leetcodeService,
    ILightOjService lightOjService,
    ISpojService spojService,
    ITimusService timusService,
    ITophService tophService,
    IUvaApiService uvaService,
    IDashboardRepository dashboardRepository): IScrapperWorkerService
{
    public async Task<SolveCountSummary> GetSolveCountAsync()
    {
        var onlineJudgeHandle = await dashboardRepository.GetOnlineJudgeHandleAsync();
        var solveCountSummary = new SolveCountSummary();
        var tasks = new Dictionary<Func<Task<int>>, Action<int>>
        {
            { () => atCoderService.GetSolveCountByAPIAsync(onlineJudgeHandle.AtCoder), result => solveCountSummary.AtCoder = result },
            { () => codechefService.GetSolveCountByScrappingAsync(onlineJudgeHandle.CodeChef), result => solveCountSummary.CodeChef = result },
            { () => codeforcesService.GetSolveCountByScrappingAsync(onlineJudgeHandle.Codeforces), result => solveCountSummary.Codeforces = result },
            { () => leetcodeService.GetSolveCountByAPIAsync(onlineJudgeHandle.LeetCode), result => solveCountSummary.LeetCode = result },
            { () => lightOjService.GetSolveCountByScrappingAsync(onlineJudgeHandle.LightOj), result => solveCountSummary.LightOj = result },
            { () => spojService.GetSolveCountByScrappingAsync(onlineJudgeHandle.Spoj), result => solveCountSummary.Spoj = result },
            { () => timusService.GetSolveCountByScrappingAsync(onlineJudgeHandle.Timus), result => solveCountSummary.Timus = result },
            { () => tophService.GetSolveCountByScrappingAsync(onlineJudgeHandle.Toph), result => solveCountSummary.Toph = result },
            { () => uvaService.GetSolveCountByAPIAsync(onlineJudgeHandle.Uva), result => solveCountSummary.Uva = result }
        };

        var stopwatch = new Stopwatch();

        StartStopWatch(stopwatch);
        var results = await Task.WhenAll(tasks.Keys.Select(taskFunc => taskFunc()));
        StopAndLogStopWatch(stopwatch);

        int index = 0;

        foreach (var setResult in tasks.Values)
        {
            setResult(results[index]);
            index++;
        }

        solveCountSummary.Total = results.Sum();

        return solveCountSummary;
    }

    [Conditional("DEBUG")]
    private static void StartStopWatch(Stopwatch stopwatch)
    {
        stopwatch.Start();
    }

    [Conditional("DEBUG")]
    private static void StopAndLogStopWatch(Stopwatch stopwatch)
    {
        stopwatch.Stop();
        long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
        Console.WriteLine($"RunTime: {elapsedMilliseconds / 1000.0} s");
    }
}

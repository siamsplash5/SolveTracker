using SolveTracker.Domain.Entities.Dashboard;

namespace SolveTracker.Application.Services.Dashboard;

public interface IDashboardService
{
    public Task<SolveCountSummary> GetTotalSolveCountSummaryAsync();
    public Task<SolveCountSummary> GetDailySolveCountSummaryAsync();
    public Task<OnlineJudgeHandle> GetOnlineJudgeHandleAsync();
    public Task<OnlineJudgeProfileLink> GetOnlineJudgeProfileLinkAsync();
    public Task<int> GetWeeklySolveCountAsync();
}

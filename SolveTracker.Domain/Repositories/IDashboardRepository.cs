using SolveTracker.Domain.Entities.Dashboard;

namespace SolveTracker.Domain.Repositories;

public interface IDashboardRepository
{
    public Task<SolveCountSummary> GetDailySolveCountSummaryAsync();
    public Task<SolveCountSummary> GetTotalSolveCountSummaryAsync();
    public Task<OnlineJudgeHandle> GetOnlineJudgeHandleAsync();
    public Task<OnlineJudgeProfileLink> GetOnlineJudgeProfileLinkAsync();
    public Task<int> GetWeeklySolveCountAsync();
}

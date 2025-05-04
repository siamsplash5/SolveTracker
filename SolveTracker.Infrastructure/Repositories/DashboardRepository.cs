using SolveTracker.Domain.Entities.Dashboard;
using SolveTracker.Domain.Repositories;

namespace SolveTracker.Infrastructure.Repositories;

public class DashboardRepository : IDashboardRepository
{
    public Task<SolveCountSummary> GetTotalSolveCountSummaryAsync()
    {
        // TODO: Will Introduce Database
        SolveCountSummary totalSolveCountSummary = new()
        {
            AtCoder = 165,
            Codeforces = 1900,
            CodeChef = 124,
            LeetCode = 477,
            LightOj = 106,
            Spoj = 28,
            Timus = 19,
            Toph = 14,
            Uva = 301,
        };

        return Task.FromResult(totalSolveCountSummary);
    }

    public Task<SolveCountSummary> GetDailySolveCountSummaryAsync()
    {
        // TODO: Will Introduce Database
        SolveCountSummary dailySolveCountSummary = new()
        {
            AtCoder = 3,
            Codeforces = 0,
            LeetCode = 1,
        };

        return Task.FromResult(dailySolveCountSummary);
    }

    public Task<OnlineJudgeHandle> GetOnlineJudgeHandleAsync()
    {
        // TODO: Will Introduce Database
        OnlineJudgeHandle onlineJudgeProfile = new()
        {
            AtCoder = "siamsplash5",
            Codeforces = "siamsplash5",
            CodeChef = "siamsplash5",
            LeetCode = "siamsplash5",
            LightOj = "siamsplash52",
            Spoj = "siamsplash5",
            Toph = "siamsplash.5",
            Timus = "291436",
            Uva = "1129555",
        };

        return Task.FromResult(onlineJudgeProfile);
    }

    public Task<OnlineJudgeProfileLink> GetOnlineJudgeProfileLinkAsync()
    {
        // TODO: Will Introduce Database
        OnlineJudgeProfileLink onlineJudgeProfileLink = new()
        {
            AtCoder = "https://atcoder.jp/users/siamsplash5",
            Codeforces = "https://codeforces.com/profile/siamsplash5",
            CodeChef = "https://www.codechef.com/users/siamsplash5",
            LeetCode = "https://leetcode.com/u/siamsplash5/",
            LightOj = "https://lightoj.com/user/siamsplash52",
            Spoj = "https://www.spoj.com/users/siamsplash5",
            Toph = "https://toph.co/u/siamsplash.5",
            Timus = "https://acm.timus.ru/author.aspx?id=291436",
            Uva = "https://uhunt.onlinejudge.org/id/1129555",
        };

        return Task.FromResult(onlineJudgeProfileLink);
    }

    public Task<int> GetWeeklySolveCountAsync()
    {
        // TODO: Will Introduce Database
        try
        {
            int weeklySolveCount = 17;
            return Task.FromResult(weeklySolveCount);
        }
        catch (Exception)
        {
            throw;
        }
    }
}

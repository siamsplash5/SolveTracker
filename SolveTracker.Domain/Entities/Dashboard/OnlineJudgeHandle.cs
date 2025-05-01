using SolveTracker.Domain.Attributes;

namespace SolveTracker.Domain.Entities.Dashboard;

public class OnlineJudgeHandle
{
    [OnlineJudgeInfo(name: OnlineJudge.Atcoder)]
    public string AtCoder { get; set; }

    [OnlineJudgeInfo(name: OnlineJudge.Codechef)]
    public string CodeChef { get; set; }

    [OnlineJudgeInfo(name: OnlineJudge.Codeforces)]
    public string Codeforces { get; set; }

    [OnlineJudgeInfo(name: OnlineJudge.Leetcode)]
    public string LeetCode { get; set; }

    [OnlineJudgeInfo(name: OnlineJudge.LightOj)]
    public string LightOj { get; set; }

    [OnlineJudgeInfo(name: OnlineJudge.SPOJ)]
    public string Spoj { get; set; }

    [OnlineJudgeInfo(name: OnlineJudge.Timus)]
    public string Timus { get; set; }

    [OnlineJudgeInfo(name: OnlineJudge.Toph)]
    public string Toph { get; set; }

    [OnlineJudgeInfo(name: OnlineJudge.UVa)]
    public string Uva { get; set; }
}

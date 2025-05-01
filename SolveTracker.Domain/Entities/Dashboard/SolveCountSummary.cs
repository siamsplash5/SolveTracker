using SolveTracker.Domain.Attributes;
using System.Diagnostics;

namespace SolveTracker.Domain.Entities.Dashboard;

[DebuggerDisplay("Total Solve: {Total}")]
public class SolveCountSummary
{
    [OnlineJudgeInfo(name: OnlineJudge.Atcoder)]
    public int AtCoder { get; set; }

    [OnlineJudgeInfo(name: OnlineJudge.Codechef)]
    public int CodeChef { get; set; }

    [OnlineJudgeInfo(name: OnlineJudge.Codeforces)]
    public int Codeforces { get; set; }

    [OnlineJudgeInfo(name: OnlineJudge.Leetcode)]
    public int LeetCode { get; set; }

    [OnlineJudgeInfo(name: OnlineJudge.LightOj)]
    public int LightOj { get; set; }

    [OnlineJudgeInfo(name: OnlineJudge.SPOJ)]
    public int Spoj { get; set; }

    [OnlineJudgeInfo(name: OnlineJudge.Timus)]
    public int Timus { get; set; }

    [OnlineJudgeInfo(name: OnlineJudge.Toph)]
    public int Toph { get; set; }

    [OnlineJudgeInfo(name: OnlineJudge.UVa)]
    public int Uva { get; set; }

    public int Total { get; set; }
}

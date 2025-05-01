namespace SolveTracker.Domain.Attributes;

[Flags]
public enum OnlineJudge
{
    Atcoder,
    Codechef,
    Codeforces,
    Leetcode,
    LightOj,
    SPOJ,
    Timus,
    Toph,
    UVa
}

[AttributeUsage(AttributeTargets.Property)]
public class OnlineJudgeInfoAttribute(OnlineJudge name) : Attribute
{
    public string Name { get; } = name.ToString();
}

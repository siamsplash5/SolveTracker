namespace SolveTracker.Domain.Entities.DailyLog;

public class DailyLogInfo
{
    public string ID { get; set; }
    public int UserId { get; set; }
    public int SolveCount { get; set; }
    public string OnlineJudge { get; set; }
}


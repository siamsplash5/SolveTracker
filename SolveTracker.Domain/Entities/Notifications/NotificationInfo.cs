namespace SolveTracker.Domain.Entities.Notifications;

public class NotificationInfo
{
    public int NotificationID { get; set; }
    public int RecipientID { get; set; }
    public int PublisherID { get; set; }
    public string PublisherName { get; set; }
    public int SolveCount { get; set; }
    public string OnlineJudge { get; set; }
    public bool SeenStatus { get; set; }
}

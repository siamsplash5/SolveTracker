namespace SolveTracker.Domain.Entities.Events;

public class NotificationEventArgs : EventArgs
{
    public string DailyLogID { get; set; }
    public int PublisherID { get; set; }
}

using SolveTracker.Domain.Entities.Notifications;

namespace SolveTracker.Domain.Repositories;

public interface INotificationRepository
{
    Task AddNotificationInfoAsync(int publisherID, string dailyLogID);
    Task<IEnumerable<NotificationInfo>> GetNotificationsAsync(int recipientID);
}

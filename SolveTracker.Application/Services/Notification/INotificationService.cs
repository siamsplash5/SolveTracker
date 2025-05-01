using SolveTracker.Domain.Entities.Notifications;

namespace SolveTracker.Application.Services.Notification;

public interface INotificationService
{
    Task<IEnumerable<NotificationInfo>> GetNotificationsAsync();
}

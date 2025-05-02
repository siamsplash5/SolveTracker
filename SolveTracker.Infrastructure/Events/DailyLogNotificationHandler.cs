using Microsoft.Extensions.Logging;
using SolveTracker.Domain.Entities.Events;
using SolveTracker.Domain.Repositories;

namespace SolveTracker.Infrastructure.Events;

public class DailyLogNotificationHandler(
    INotificationRepository notificationRepository,
    ILogger<DailyLogNotificationHandler> logger)
{
    public async void HandleDailyLogAddedNotification(object sender, NotificationEventArgs e)
    {
        try
        {
            await notificationRepository.AddNotificationInfoAsync(e.PublisherID, e.DailyLogID);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
        }
    }
}

using SolveTracker.Events.EventModels;
using SolveTracker.Repositories.Notifications;
using System;

namespace SolveTracker.Events.Handlers
{
    public class DailyLogNotificationHandler
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly ILogger<DailyLogNotificationHandler> _logger;

        public DailyLogNotificationHandler(
            INotificationRepository notificationRepository,
            ILogger<DailyLogNotificationHandler> logger)
        {
            _notificationRepository = notificationRepository;
            _logger = logger;
        }

        public async void HandleDailyLogAddedNotification(object? sender, NotificationEventArgs e)
        {
            try
            {
                await _notificationRepository.AddNotificationInfoAsync(e.PublisherID, e.DailyLogID);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}

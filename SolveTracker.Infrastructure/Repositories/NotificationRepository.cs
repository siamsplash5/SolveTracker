using SolveTracker.DBContext;
using SolveTracker.Models.Notifications;

namespace SolveTracker.Repositories.Notifications
{
    public class NotificationRepository(IDapperDBContext dapperDBContext) : INotificationRepository
    {
        private readonly string _addNotificationInfoSP = "NOTIFICATION_AddNotificationInfo";
        private readonly string _getNotificationInfoSP = "NOTIFICATION_GetNotificationInfo";

        public async Task AddNotificationInfoAsync(int publisherID, string dailyLogID)
        {
            await dapperDBContext.ExecuteAsync(new {publisherID, dailyLogID}, _addNotificationInfoSP);
        }

        public async Task<IEnumerable<NotificationInfo>> GetNotificationsAsync(int recipientID)
        {
            return await dapperDBContext.GetInfoListAsync<object, NotificationInfo>(new
            {
                RecipientID = recipientID
            }, _getNotificationInfoSP);
        }
    }
}

using Microsoft.AspNetCore.Http;
using SolveTracker.Domain.Entities.Notifications;
using SolveTracker.Domain.Repositories;
using System.Security.Claims;

namespace SolveTracker.Application.Services.Notification;

public class NotificationService(
    INotificationRepository notificationRepository,
    IHttpContextAccessor httpContextAccessor): INotificationService
{
    public async Task<IEnumerable<NotificationInfo>> GetNotificationsAsync()
    {
        var userId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Sid)?.Value;
        return await notificationRepository.GetNotificationsAsync(Convert.ToInt32(userId));
    }
}

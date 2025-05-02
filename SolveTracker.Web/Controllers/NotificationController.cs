using Microsoft.AspNetCore.Mvc;
using SolveTracker.Application.Services.Notification;
using SolveTracker.Web.Models.Notification;

namespace SolveTracker.Web.Controllers;

public class NotificationController (INotificationService notificationService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var notifications = await notificationService.GetNotificationsAsync();

        var notificationList = new List<NotificationViewModel>();

        foreach(var notification in notifications)
        {
            notificationList.Add(new NotificationViewModel
            {
                NotificationID = notification.NotificationID,
                HeaderMessage = $"{notification.PublisherName} has updated a daily log",
                BodyMessage = $"Solved {notification.SolveCount} problem(s) on {notification.OnlineJudge}.",
            });
        }
        return View(notificationList);
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SolveTracker.Domain.Entities.DailyLog;
using SolveTracker.Domain.Entities.Events;
using SolveTracker.Domain.Repositories;
using System.Security.Claims;

namespace SolveTracker.Application.Services.DailyLog;

public class DailyLogService(
        IDailyLogRepository dailyLogRepository,
        ILogger<DailyLogService> logger,
        IHttpContextAccessor httpContextAccessor) : IDailyLogService
{
    public event EventHandler<NotificationEventArgs> DailylogAdded;

    public async Task AddDailyLogAsync(DailyLogInfo dailyLog)
    {
        try
        {
            string userId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Sid)?.Value;
            dailyLog.ID = Guid.NewGuid().ToString();
            dailyLog.UserId = Convert.ToInt32(userId);
            await dailyLogRepository.AddDailyLogAsync(dailyLog);
            OnDailyLogAddComplete(dailyLog.ID, dailyLog.UserId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            throw;
        }
    }

    private void OnDailyLogAddComplete(string dailyLogId, int publisherId)
    {
        DailylogAdded?.Invoke(this, new NotificationEventArgs
        {
            DailyLogID = dailyLogId,
            PublisherID = publisherId
        });
    }
}

using SolveTracker.Domain.Entities.DailyLog;
using SolveTracker.Domain.Entities.Events;

namespace SolveTracker.Application.Services.DailyLog;

public interface IDailyLogService
{
    public event EventHandler<NotificationEventArgs> DailylogAdded;
    Task AddDailyLogAsync(DailyLogInfo dailyLog);
}

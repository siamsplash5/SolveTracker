using SolveTracker.Domain.Entities.DailyLog;

namespace SolveTracker.Domain.Repositories;

public interface IDailyLogRepository
{
    Task AddDailyLogAsync(DailyLogInfo dailyLog);
}

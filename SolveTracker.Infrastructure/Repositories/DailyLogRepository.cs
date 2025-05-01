using SolveTracker.DBContext;
using SolveTracker.Domain.Entities.DailyLog;

namespace SolveTracker.Repositories.DailyLog;

public class DailyLogRepository(IDapperDBContext dapperDBContext) : IDailyLogRepository
{
    private readonly string _addDailyLogInfoSP = "DAILYLOG_AddDailyLogInfo";

    public async Task AddDailyLogAsync(DailyLogInfo dailyLog)
    {
        await dapperDBContext.ExecuteAsync(dailyLog, _addDailyLogInfoSP);
    }
}

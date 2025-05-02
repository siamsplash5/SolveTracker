using SolveTracker.Domain.Entities.DailyLog;
using SolveTracker.Domain.Repositories;
using SolveTracker.Infrastructure.DBContext;

namespace SolveTracker.Infrastructure.Repositories;

public class DailyLogRepository(IDapperDBContext dapperDBContext) : IDailyLogRepository
{
    private readonly string _addDailyLogInfoSP = "DAILYLOG_AddDailyLogInfo";

    public async Task AddDailyLogAsync(DailyLogInfo dailyLog)
    {
        await dapperDBContext.ExecuteAsync(dailyLog, _addDailyLogInfoSP);
    }
}

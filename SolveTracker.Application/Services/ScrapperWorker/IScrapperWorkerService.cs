using SolveTracker.Domain.Entities.Dashboard;

namespace SolveTracker.Application.Services.ScrapperWorker;

public interface IScrapperWorkerService
{
    Task<SolveCountSummary> GetSolveCountAsync();
}

namespace SolveTracker.Infrastructure.DBContext;

public interface IDapperDBContext
{
    Task ExecuteAsync<TParams>(TParams obj, string spName);
    Task<TResult> GetInfoAsync<TParam, TResult>(TParam obj, string spName);
    Task<IEnumerable<TResult>> GetInfoListAsync<TParam, TResult>(TParam obj, string spName);
}

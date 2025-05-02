using Microsoft.Extensions.DependencyInjection;
using SolveTracker.Domain.ApiServices;
using SolveTracker.Domain.Repositories;
using SolveTracker.Domain.Scrappers;
using SolveTracker.Infrastructure.ApiServices;
using SolveTracker.Infrastructure.DBContext;
using SolveTracker.Infrastructure.Events;
using SolveTracker.Infrastructure.Repositories;
using SolveTracker.Infrastructure.Scrappers;

namespace SolveTracker.Infrastructure.Extensions;

public static class InfrastructureServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IDapperDBContext, DapperDBContext>();

        services.AddScoped<IDashboardRepository, DashboardRepository>();
        services.AddScoped<ILoginRepository, LoginRepository>();
        services.AddScoped<IRegistrationRepository, RegistrationRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<IDailyLogRepository, DailyLogRepository>();

        services.AddScoped<IAtcoderApiService, AtcoderApiService>();
        services.AddScoped<IBeecrowdService, BeecrowdService>();
        services.AddScoped<ICodechefService, CodechefService>();
        services.AddScoped<ICodeforcesService, CodeforcesService>();
        services.AddScoped<ICsesService, CsesService>();
        services.AddScoped<ILeetcodeApiService, LeetcodeApiService>();
        services.AddScoped<ILightOjService, LightOjService>();
        services.AddScoped<ISpojService, SpojService>();
        services.AddScoped<ITophService, TophService>();
        services.AddScoped<ITimusService, TimusService>();
        services.AddScoped<IUvaApiService, UvaApiService>();
        services.AddScoped<IWebScrapperService, WebScrapperService>();

        services.AddScoped<DailyLogNotificationHandler>();
    }
}

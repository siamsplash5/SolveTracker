using Microsoft.Extensions.DependencyInjection;
using SolveTracker.Application.Services.DailyLog;
using SolveTracker.Application.Services.Dashboard;
using SolveTracker.Application.Services.Login;
using SolveTracker.Application.Services.Notification;
using SolveTracker.Application.Services.Registration;
using SolveTracker.Application.Services.ScrapperWorker;

namespace SolveTracker.Application.Extensions;

public static class ApplicationServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IDashboardService, DashboardService>();
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<IRegistrationService, RegistrationService>();
        services.AddScoped<IDailyLogService, DailyLogService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IScrapperWorkerService, ScrapperWorkerService>();
    }
}

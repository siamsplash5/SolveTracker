using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SolveTracker.Application.Services.DailyLog;
using SolveTracker.Domain.Entities.Common;
using SolveTracker.Domain.Entities.DailyLog;
using SolveTracker.Infrastructure.Events;
using SolveTracker.Web.Extensions.Attributes;
using SolveTracker.Web.Models.Common;
using SolveTracker.Web.Models.DailyLog;

namespace SolveTracker.Web.Controllers;

[RoleAuthorize([UserRole.Programmer])]
public class DailyLogController(
    IMapper mapper, 
    IDailyLogService dailyLogService,
    DailyLogNotificationHandler handler) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(DailyLogViewModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var dailyLogInfo = mapper.Map<DailyLogInfo>(model);
                dailyLogService.DailylogAdded += handler.HandleDailyLogAddedNotification;
                await dailyLogService.AddDailyLogAsync(dailyLogInfo);
                return RedirectToAction("Index", "Dashboard");
            }
            return View(model);
        }
        catch (Exception)
        {
            var errorViewModel = new ErrorViewModel();
            return View("../Common/Error", errorViewModel);
        }
    }
}

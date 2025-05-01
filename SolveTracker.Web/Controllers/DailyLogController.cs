using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SolveTracker.Events.Handlers;
using SolveTracker.Models.Common;
using SolveTracker.Models.DailyLog;
using SolveTracker.Services.DailyLog;
using SolveTracker.Utilities.Attributes;
using SolveTracker.ViewModels.Common;
using SolveTracker.ViewModels.DailyLog;

namespace SolveTracker.Controllers
{
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
}

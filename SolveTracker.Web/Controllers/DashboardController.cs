using Microsoft.AspNetCore.Mvc;
using SolveTracker.Models.Common;
using SolveTracker.Models.Dashboard;
using SolveTracker.Services.Dashboard;
using SolveTracker.Utilities.Attributes;
using SolveTracker.ViewModels.Common;
using SolveTracker.ViewModels.Dashboard;
using System.Data;
using System.Reflection;

namespace SolveTracker.Controllers
{
    [RoleAuthorizeAttribute(UserRole.All)]
    public class DashboardController(IDashboardService dashboardService) : Controller
    {
        #region Public Methods

        public async Task<IActionResult> Index()
        {
            try
            {
                if (User.IsInRole("Programmer"))
                {
                    var model = await GetProgrammerViewModelAsync();
                    return View("ProgrammerDashboard", model);
                }
                else if (User.IsInRole("Teacher"))
                {
                    var model = await GetTeacherViewModelAsync();
                    return View("TeacherDashboard", model);
                }

                return View("~/Views/Common/AccessDenied.cshtml", null);
            }
            catch (Exception)
            {
                var errorViewModel = new ErrorViewModel();
                return View("Error", errorViewModel);
            }
        }

        #endregion

        #region Private Methods

        private async Task<ProgrammerViewModel> GetProgrammerViewModelAsync()
        {
            var onlineJudgeHandle = await dashboardService.GetOnlineJudgeHandleAsync();
            var onlineJudgeProfileLink = await dashboardService.GetOnlineJudgeProfileLinkAsync();
            int weeklySolveCount = await dashboardService.GetWeeklySolveCountAsync();
            var dailySolveCountSummary = await dashboardService.GetDailySolveCountSummaryAsync();
            var totalSolveCountSummary = await dashboardService.GetTotalSolveCountSummaryAsync();
            var formattedDailySolveCountSummary = GetFormattedDailySolveCountSummary(dailySolveCountSummary);
            var formattedTotalSolveCountSummary = GetFormattedTotalSolveCountSummary(totalSolveCountSummary, onlineJudgeHandle, onlineJudgeProfileLink);

            var model = new ProgrammerViewModel
            {
                DisplayName = User.Identity.Name,
                DailySolveCount = dailySolveCountSummary.Total,
                WeeklySolveCount = weeklySolveCount,
                TotalSolveCount = totalSolveCountSummary.Total,
                DailySolveCountSummary = formattedDailySolveCountSummary,
                TotalSolveCountSummary = formattedTotalSolveCountSummary,
            };

            return model;
        }

        private async Task<TeacherViewModel> GetTeacherViewModelAsync()
        {
            await Task.Delay(100);
            var model = new TeacherViewModel
            {
                DisplayName = User.Identity.Name,
            };

            return model;
        }

        private static List<DailySolveCountSummary> GetFormattedDailySolveCountSummary(SolveCountSummary summary)
        {
            int index = 1;
            var result = new List<DailySolveCountSummary>();
            var summaryProperties = summary
                    .GetType()
                    .GetProperties()
                    .Where(property => property.Name != "Total");

            foreach (var property in summaryProperties)
            {
                var solveCount = (int)property.GetValue(summary);
                if (solveCount == 0) continue;
                var attr = property.GetCustomAttribute<OnlineJudgeInfoAttribute>(false);
                var judgeName = attr.Name;

                result.Add(new DailySolveCountSummary
                {
                    Index = index,
                    JudgeName = judgeName,
                    SolveCount = solveCount
                });

                index++;
            }

            return result;
        }

        private static List<TotalSolveCountSummary> GetFormattedTotalSolveCountSummary(SolveCountSummary summary, OnlineJudgeHandle onlineJudgeHandle, OnlineJudgeProfileLink onlineJudgeProfileLink)
        {
            int index = 1;
            var result = new List<TotalSolveCountSummary>();
            var summaryProperties = summary
                    .GetType()
                    .GetProperties()
                    .Where(property => property.Name != "Total");

            foreach (var property in summaryProperties)
            {
                var solveCount = (int)property.GetValue(summary);
                if (solveCount == 0) continue;
                var attr = property.GetCustomAttribute<OnlineJudgeInfoAttribute>(false);
                var judgeName = attr.Name;

                var handle = (string)onlineJudgeHandle
                    .GetType()
                    .GetProperties()
                    .Where(property => property.GetCustomAttribute<OnlineJudgeInfoAttribute>(false).Name == judgeName)
                    .FirstOrDefault()
                    ?.GetValue(onlineJudgeHandle);

                var profileLink = (string)onlineJudgeProfileLink
                    .GetType()
                    .GetProperties()
                    .Where(property => property.GetCustomAttribute<OnlineJudgeInfoAttribute>(false).Name == judgeName)
                    .FirstOrDefault()
                    ?.GetValue(onlineJudgeProfileLink);

                result.Add(new TotalSolveCountSummary
                {
                    Index = index,
                    JudgeName = judgeName,
                    OnlineJudgeHandle = handle,
                    ProfileLink = profileLink,
                    SolveCount = solveCount
                });

                index++;
            }

            return result;
        }

        #endregion
    }
}

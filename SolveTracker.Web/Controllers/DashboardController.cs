using Microsoft.AspNetCore.Mvc;
using SolveTracker.Application.Services.Dashboard;
using SolveTracker.Domain.Attributes;
using SolveTracker.Domain.Entities.Common;
using SolveTracker.Domain.Entities.Dashboard;
using SolveTracker.Web.Extensions.Attributes;
using SolveTracker.Web.Models.Common;
using SolveTracker.Web.Models.Dashboard;
using System.Data;
using System.Reflection;

namespace SolveTracker.Web.Controllers;

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
                ProgrammerViewModel model = await GetProgrammerViewModelAsync();
                return View("ProgrammerDashboard", model);
            }
            else if (User.IsInRole("Teacher"))
            {
                TeacherViewModel model = await GetTeacherViewModelAsync();
                return View("TeacherDashboard", model);
            }

            return View("~/Views/Common/AccessDenied.cshtml", null);
        }
        catch (Exception)
        {
            ErrorViewModel errorViewModel = new();
            return View("Error", errorViewModel);
        }
    }

    #endregion

    #region Private Methods

    private async Task<ProgrammerViewModel> GetProgrammerViewModelAsync()
    {
        OnlineJudgeHandle onlineJudgeHandle = await dashboardService.GetOnlineJudgeHandleAsync();
        OnlineJudgeProfileLink onlineJudgeProfileLink = await dashboardService.GetOnlineJudgeProfileLinkAsync();
        
        int weeklySolveCount = await dashboardService.GetWeeklySolveCountAsync();
        SolveCountSummary dailySolveCountSummary = await dashboardService.GetDailySolveCountSummaryAsync();
        SolveCountSummary totalSolveCountSummary = await dashboardService.GetTotalSolveCountSummaryAsync();

        List<DailySolveCountSummary> formattedDailySolveCountSummary = GetFormattedDailySolveCountSummary(dailySolveCountSummary);
        List<TotalSolveCountSummary> formattedTotalSolveCountSummary = GetFormattedTotalSolveCountSummary(totalSolveCountSummary, onlineJudgeHandle, onlineJudgeProfileLink);

        ProgrammerViewModel model = new()
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
        TeacherViewModel model = new()
        {
            DisplayName = User.Identity.Name,
        };

        return model;
    }

    private static List<DailySolveCountSummary> GetFormattedDailySolveCountSummary(SolveCountSummary summary)
    {
        int index = 1;
        List<DailySolveCountSummary> result = [];
        IEnumerable<PropertyInfo> summaryProperties = summary
                .GetType()
                .GetProperties()
                .Where(property => property.Name != "Total");

        foreach (PropertyInfo property in summaryProperties)
        {
            int solveCount = (int)property.GetValue(summary);
            if (solveCount == 0)
            {
                continue;
            }

            OnlineJudgeInfoAttribute attr = property.GetCustomAttribute<OnlineJudgeInfoAttribute>(false);
            string judgeName = attr.Name;

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
        List<TotalSolveCountSummary> result = [];
        IEnumerable<PropertyInfo> summaryProperties = summary
                .GetType()
                .GetProperties()
                .Where(property => property.Name != "Total");

        foreach (PropertyInfo property in summaryProperties)
        {
            int solveCount = (int)property.GetValue(summary);
            if (solveCount == 0)
            {
                continue;
            }

            OnlineJudgeInfoAttribute attr = property.GetCustomAttribute<OnlineJudgeInfoAttribute>(false);
            string judgeName = attr.Name;

            string handle = (string)onlineJudgeHandle
                .GetType()
                .GetProperties()
                .Where(property => property.GetCustomAttribute<OnlineJudgeInfoAttribute>(false).Name == judgeName)
                .FirstOrDefault()
                ?.GetValue(onlineJudgeHandle);

            string profileLink = (string)onlineJudgeProfileLink
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

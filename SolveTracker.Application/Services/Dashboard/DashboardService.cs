﻿using SolveTracker.Domain.Entities.Dashboard;
using SolveTracker.Domain.Repositories;
using System.Reflection;

namespace SolveTracker.Application.Services.Dashboard;

public class DashboardService(IDashboardRepository dashboardRepository) : IDashboardService
{
    public async Task<SolveCountSummary> GetTotalSolveCountSummaryAsync()
    {
        try
        {
            SolveCountSummary totalSolveCountSummary = await dashboardRepository.GetTotalSolveCountSummaryAsync();
            totalSolveCountSummary.Total = GetSumOfIntegerProperties<SolveCountSummary>(totalSolveCountSummary);

            return totalSolveCountSummary;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<SolveCountSummary> GetDailySolveCountSummaryAsync()
    {
        try
        {
            SolveCountSummary dailySolveCountSummary = await dashboardRepository.GetDailySolveCountSummaryAsync();
            dailySolveCountSummary.Total = GetSumOfIntegerProperties<SolveCountSummary>(dailySolveCountSummary);
            return dailySolveCountSummary;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<OnlineJudgeHandle> GetOnlineJudgeHandleAsync()
    {
        try
        {
            OnlineJudgeHandle onlineJudgeHandle = await dashboardRepository.GetOnlineJudgeHandleAsync();
            return onlineJudgeHandle;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public Task<OnlineJudgeProfileLink> GetOnlineJudgeProfileLinkAsync()
    {
        try
        {
            Task<OnlineJudgeProfileLink> onlineJudgeProfileLink = dashboardRepository.GetOnlineJudgeProfileLinkAsync();
            return onlineJudgeProfileLink;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public Task<int> GetWeeklySolveCountAsync()
    {
        try
        {
            Task<int> weeklySolveCount = dashboardRepository.GetWeeklySolveCountAsync();
            return weeklySolveCount;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private static int GetSumOfIntegerProperties<T>(T obj)
    {
        int sum = obj.GetType()
                     .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                     .Where(p => p.PropertyType == typeof(int))
                     .Sum(p => (int)p.GetValue(obj));

        return sum;
    }
}

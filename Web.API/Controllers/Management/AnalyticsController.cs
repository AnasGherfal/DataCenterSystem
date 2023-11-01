using Core.Constants;
using Core.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Web.API.Abstracts;
using Web.API.Features.Management.AnalyticsManagement.FetchDashboardStatistics;
using Web.API.Filters;

namespace Web.API.Controllers.Management;


[VerifiedAdmin(SystemPermissions.AnalyticsManagement)]
[ApiController]
public class AnalyticsController : ManagementController
{
    [HttpGet("Dashboard-Counters")]
    public async Task<ContentResponse<FetchDashboardStatisticsQueryResponse>> DashboardCounters() 
        => await Mediator.Send(new FetchDashboardStatisticsQuery());
}
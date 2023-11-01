
using Core.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Web.API.Abstracts;
using Web.API.Features.Consumer.AnalyticsManagement.FetchDashboardStatistics;
using Web.API.Filters;

namespace Web.API.Controllers.Consumer;

[VerifiedCustomer]
[ApiController]
public class AnalyticsController : ConsumerController
{
    [HttpGet("Dashboard-Counters")]
    public async Task<ContentResponse<FetchDashboardStatisticsQueryResponse>> DashboardCounters() 
        => await Mediator.Send(new FetchDashboardStatisticsQuery());
}
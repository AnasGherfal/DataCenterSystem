using Core.Constants;
using Core.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Web.API.Abstracts;
using Web.API.Features.AnalyticsManagement.FetchDashboardStatistics;
using Web.API.Features.AuditManagement.FetchAudits;
using Web.API.Features.AuditManagement.FetchProjectionById;
using Web.API.Filters;

namespace Web.API.Controllers.Management;


// [VerifiedAdmin(SystemPermissions.SuperAdmin)]
[ApiController]
public class AuditsController : ManagementController
{
    [HttpGet]
    public async Task<PagedResponse<FetchAuditsQueryResponse>> FetchEvents([FromQuery] FetchAuditsQuery request) 
        => await Mediator.Send(request);

    [HttpGet("{id}")]
    public async Task<ContentResponse<FetchProjectionByIdQueryResponse>> FetchEvents(string id, [FromQuery] FetchProjectionByIdQuery request)
    {
        request.SetId(id);
        return await Mediator.Send(request);
    }
}
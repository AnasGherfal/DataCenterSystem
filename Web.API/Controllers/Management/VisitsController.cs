using Core.Constants;
using Core.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Web.API.Abstracts;
using Web.API.Features.Management.VisitsManagement.SignVisit;
using Web.API.Features.VisitsManagement.CreateVisit;
using Web.API.Features.VisitsManagement.DeleteVisit;
using Web.API.Features.VisitsManagement.EndVisit;
using Web.API.Features.VisitsManagement.FetchVisitById;
using Web.API.Features.VisitsManagement.FetchVisits;
using Web.API.Features.VisitsManagement.StartVisit;
using Web.API.Filters;

namespace Web.API.Controllers.Management;

// [VerifiedAdmin(SystemPermissions.VisitsManagement)]
[ApiController]
public class VisitsController : ManagementController
{
    [HttpPost]
    public async Task<MessageResponse> Create([FromBody] CreateVisitCommand request) 
        => await Mediator.Send(request);
    
    [HttpGet]
    public async Task<PagedResponse<FetchVisitsQueryResponse>> Fetch([FromQuery] FetchVisitsQuery request) 
        => await Mediator.Send(request);
    
    [HttpGet("{id}")]
    public async Task<ContentResponse<FetchVisitByIdQueryResponse>> FetchById(string id)
        => await Mediator.Send(new FetchVisitByIdQuery()
        {
            Id = id,
        });
    
    [HttpPut("{id}/sign")]
    public async Task<MessageResponse> Sign(string id, [FromBody] SignVisitCommand request)
    {
        request.SetId(id);
        return await Mediator.Send(request);
    }

    [HttpDelete("{id}")]
    public async Task<MessageResponse> Delete(string id)
        => await Mediator.Send(new DeleteVisitCommand()
        {
            Id = id,
        });
}
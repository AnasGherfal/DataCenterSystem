using Core.Constants;
using Core.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Web.API.Abstracts;
using Web.API.Features.TimeShiftManagement.CreateTimeShift;
using Web.API.Features.TimeShiftManagement.DeleteTimeShift;
using Web.API.Features.TimeShiftManagement.FetchTimeShiftById;
using Web.API.Features.TimeShiftManagement.FetchTimeShifts;
using Web.API.Features.TimeShiftManagement.UpdateTimeShift;
using Web.API.Filters;

namespace Web.API.Controllers.Management;

// [VerifiedAdmin(SystemPermissions.TimeShiftManagement)]
[ApiController]
public class TimeShiftsController : ManagementController
{
    [HttpPost]
    public async Task<MessageResponse> Create([FromBody] CreateTimeShiftCommand request) 
        => await Mediator.Send(request);
    
    [HttpGet]
    public async Task<PagedResponse<FetchTimeShiftsQueryResponse>> Fetch([FromQuery] FetchTimeShiftsQuery request) 
        => await Mediator.Send(request);
    
    [HttpGet("{id}")]
    public async Task<ContentResponse<FetchTimeShiftByIdQueryResponse>> FetchById(string id)
        => await Mediator.Send(new FetchTimeShiftByIdQuery()
        {
            Id = id,
        });

    [HttpPut("{id}")]
    public async Task<MessageResponse> Update(string id, [FromBody] UpdateTimeShiftCommand request)
    {
        request.SetId(id);
        return await Mediator.Send(request);
    }

    [HttpDelete("{id}")]
    public async Task<MessageResponse> Delete(string id) 
        => await Mediator.Send(new DeleteTimeShiftCommand()
        {
            Id = id,
        });
}
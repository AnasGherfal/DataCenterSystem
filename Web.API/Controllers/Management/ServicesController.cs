using Common.Constants;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Web.API.Abstracts;
using Web.API.Features.ServiceManagement.CreateService;
using Web.API.Features.ServiceManagement.DeleteService;
using Web.API.Features.ServiceManagement.FetchServiceById;
using Web.API.Features.ServiceManagement.FetchServices;
using Web.API.Features.ServiceManagement.LockService;
using Web.API.Features.ServiceManagement.UnlockService;
using Web.API.Features.ServiceManagement.UpdateService;
using Web.API.Filters;

namespace Web.API.Controllers.Management;

[RoleBasedPermission(SystemPermissions.ServiceManegment)]
[ApiController]
public class ServicesController : ManagementController
{
    [HttpPost]
    public async Task<MessageResponse> Create([FromBody] CreateServiceCommand request) 
        => await Mediator.Send(request);
    
    [HttpGet]
    public async Task<PagedResponse<FetchServicesQueryResponse>> Fetch([FromQuery] FetchServicesQuery request) 
        => await Mediator.Send(new FetchServicesQuery(request.PageNumber, request.PageSize));
    
    [HttpGet("{id}")]
    public async Task<ContentResponse<FetchServiceByIdQueryResponse>> FetchById(string id)
        => await Mediator.Send(new FetchServiceByIdQuery(id));

    [HttpPut("{id}")]
    public async Task<MessageResponse> Update(string id, [FromBody] UpdateServiceCommand request)
    {
        request.SetId(id);
        return await Mediator.Send(request);
    }

    [HttpPut("{id}/lock")]
    public async Task<MessageResponse> Lock(string id) 
        => await Mediator.Send(new LockServiceCommand()
        {
            Id = id,
        });
    
    [HttpPut("{id}/unlock")]
    public async Task<MessageResponse> UnLock(string id) 
        => await Mediator.Send(new UnlockServiceCommand()
        {
            Id = id,
        });
    
    [HttpDelete("{id}")]
    public async Task<MessageResponse> Delete(string id) 
        => await Mediator.Send(new DeleteServiceCommand()
        {
            Id = id,
        });
}
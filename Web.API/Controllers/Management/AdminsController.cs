using Common.Constants;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Web.API.Abstracts;
using Web.API.Features.AdminsManagement.CreateAdmin;
using Web.API.Features.AdminsManagement.DeleteAdminById;
using Web.API.Features.AdminsManagement.FetchAdminById;
using Web.API.Features.AdminsManagement.FetchAdmins;
using Web.API.Features.AdminsManagement.LockAdminById;
using Web.API.Features.AdminsManagement.UnlockAdminById;
using Web.API.Features.AdminsManagement.UpdateAdmin;
using Web.API.Filters;

namespace Web.API.Controllers.Management;

[RoleBasedPermission(SystemPermissions.SuperAdmin)]
[ApiController]
public class AdminsController : ManagementController
{
    [HttpPost]
    public async Task<ContentResponse<CreateAdminCommandResponse>> Create([FromBody] CreateAdminCommand request) 
        => await Mediator.Send(request);
    
    [HttpGet]
    public async Task<PagedResponse<FetchAdminsQueryResponse>> Fetch([FromQuery] FetchAdminsQuery request) 
        => await Mediator.Send(new FetchAdminsQuery(request.PageNumber, request.PageSize));
    
    [HttpGet("{id}")]
    public async Task<ContentResponse<FetchAdminByIdQueryResponse>> FetchById(string id) 
        => await Mediator.Send(new FetchAdminByIdQuery(id));

    [HttpPut("{id}")]
    public async Task<MessageResponse> Update(string id, [FromBody] UpdateAdminCommand request)
    {
        request.SetId(id);
        return await Mediator.Send(request);
    }

    [HttpPut("{id}/lock")]
    public async Task<MessageResponse> Lock(string id) 
        => await Mediator.Send(new LockAdminByIdCommand(id));
    
    [HttpPut("{id}/unlock")]
    public async Task<MessageResponse> UnLock(string id) 
        => await Mediator.Send(new UnlockAdminByIdCommand(id));
    
    [HttpDelete("{id}")]
    public async Task<MessageResponse> Delete(string id) 
        => await Mediator.Send(new DeleteAdminByIdCommand(id));
}
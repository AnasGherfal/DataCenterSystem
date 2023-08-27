using Core.Constants;
using Core.Dtos;
using Microsoft.AspNetCore.Mvc;
using Web.API.Abstracts;
using Web.API.Features.AdminsManagement.FetchPermissions;
using Web.API.Filters;

namespace Web.API.Controllers.Management;

[VerifiedAdmin(SystemPermissions.SuperAdmin)]
[ApiController]
public class ListsController : ManagementController
{
    [HttpGet("admin-permissions")]
    public async Task<ListResponse<FetchPermissionsQueryResponse>> Fetch() 
        => await Mediator.Send(new FetchPermissionsQuery());
}
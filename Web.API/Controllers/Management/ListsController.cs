using Common.Constants;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Web.API.Abstracts;
using Web.API.Features.AdminsManagement.CreateAdmin;
using Web.API.Features.AdminsManagement.DeleteAdminById;
using Web.API.Features.AdminsManagement.FetchAdminById;
using Web.API.Features.AdminsManagement.FetchAdmins;
using Web.API.Features.AdminsManagement.FetchPermissions;
using Web.API.Features.AdminsManagement.LockAdminById;
using Web.API.Features.AdminsManagement.UnlockAdminById;
using Web.API.Features.AdminsManagement.UpdateAdmin;
using Web.API.Filters;

namespace Web.API.Controllers.Management;

// [VerifiedAdmin(SystemPermissions.None)]
[ApiController]
public class ListsController : ManagementController
{
    [HttpGet("admin-permissions")]
    public async Task<ListResponse<FetchPermissionsQueryResponse>> Fetch() 
        => await Mediator.Send(new FetchPermissionsQuery());
}
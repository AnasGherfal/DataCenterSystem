using Core.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Web.API.Abstracts;
using Web.API.Features.VisitTypesManagement.FetchVisitTypes;
namespace Web.API.Controllers.Management;

// [VerifiedAdmin(SystemPermissions.SuperAdmin)]
[ApiController]
public class VisitTypesController : ManagementController
{
    [HttpGet]
    public async Task<ListResponse<FetchVisitTypesQueryResponse>> Fetch() 
        => await Mediator.Send(new FetchVisitTypesQuery());
}
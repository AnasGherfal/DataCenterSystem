using Core.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.API.Abstracts;
using Web.API.Features.Authentication.ChangePassword;
using Web.API.Features.Authentication.Profile;
using Web.API.Filters;

namespace Web.API.Controllers.Management;


[VerifiedAdmin]
[ApiController]
public class AuthController : ManagementController
{
    [HttpGet("Profile")]
    public async Task<ContentResponse<FetchProfileQueryResponse>> Profile() 
        => await Mediator.Send(new FetchProfileQuery());
    
    
    [HttpPost("Change-Password")]
    public async Task<MessageResponse> ChangePassword([FromBody] ChangePasswordCommand request) 
        => await Mediator.Send(request);
}
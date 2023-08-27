using Core.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.API.Abstracts;
using Web.API.Features.Authentication.ChangePassword;

namespace Web.API.Controllers.Management;


[Authorize]
[ApiController]
public class AuthController : ManagementController
{
    [HttpPost("Change-Password")]
    public async Task<MessageResponse> ChangePassword([FromBody] ChangePasswordCommand request) 
        => await Mediator.Send(request);
}
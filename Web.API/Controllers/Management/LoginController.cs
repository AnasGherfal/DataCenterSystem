using Core.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.API.Abstracts;
using Web.API.Features.Authentication.Login;

namespace Web.API.Controllers.Management;


[ApiController]
public class LoginController : ManagementController
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<ContentResponse<LoginCommandResponse>> Login([FromBody] LoginCommand request) 
        => await Mediator.Send(request);
}
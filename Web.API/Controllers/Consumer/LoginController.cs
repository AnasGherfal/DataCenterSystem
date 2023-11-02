using Core.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.API.Abstracts;
using Web.API.Features.Consumer.Login;

namespace Web.API.Controllers.Consumer;


[ApiController]
public class LoginController : ConsumerController
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<ContentResponse<LoginCommandResponse>> Login([FromBody] LoginCommand request) 
        => await Mediator.Send(request);
}
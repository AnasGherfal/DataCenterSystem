using System.Net;
using Core.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers;

[ApiController]
[Route("errorStatusCodes")]
public class ErrorsController : ControllerBase
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("{code:int}")]
    public IActionResult Error(int? code)
    {
        var statusCode = (HttpStatusCode) (code ?? 500);
        var response = new MessageResponse()
        {
            Msg = statusCode.ToString(),
        };
        return StatusCode((int) statusCode, response);
    }
}
using Infrastructure.Models;
using ManagementAPI.Dtos.Visit;
using ManagementAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;

namespace ManagementAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VisitController : ControllerBase
{
    private readonly IVisitService _service;
    public VisitController(IVisitService service)
    {
        _service = service;
    }

 /********* 
    [HttpPost]
    public async Task<OperationResponse> Create([FromBody] CreateVisitRequestDto request)
    {
        var result = await _service.Create(request);
        return Ok(result);
    }
 */
}

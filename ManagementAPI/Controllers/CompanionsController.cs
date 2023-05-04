using ManagementAPI.Dtos.Companion;
using ManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ManagementAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompanionsController : ControllerBase
{
    private readonly ICompanionService _service;
    public CompanionsController(ICompanionService service)
    {
        _service = service;  
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCompanionRequestDto request) 
        => Ok(await _service.Create(request));

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] FetchCompanionRequestDto filter)
        => Ok(await _service.GetAll(filter));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCompanionRequestDto request) 
        => Ok(await _service.Update(id, request));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) 
        => Ok(await _service.Delete(id));
}

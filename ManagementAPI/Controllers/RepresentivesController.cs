using ManagementAPI.Dtos.Representive;
using ManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ManagementAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
//TODO: REVIEW [Bonus]: Correct Spelling Controller, Service, Interface => Representatives
public class RepresentivesController : ControllerBase
{
    private readonly IRepresentiveService _service;
    public RepresentivesController(IRepresentiveService service)
    {
        _service = service;
    }
    
    [HttpGet]
    //TODO: REVIEW [Bonus]: Add CustomerId Query to allow filtering by CustomerId
    public async Task<IActionResult> GetAll([FromQuery] FetchRepresentiveRequestDto filter) 
        => Ok(await _service.GetAll(filter));
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRepresentiveRequestDto request) 
        => Ok(await _service.Create(request));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateRepresentiveRequestDto request)
        => Ok(await _service.Update(id, request));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
        => Ok(await _service.Delete(id));

    [HttpPut("{id}/lock")]
    public async Task<IActionResult> Lock(int id) 
        => Ok(await _service.Lock(id));
    
    [HttpPut("{id}/Unlock")]
    public async Task<IActionResult> Unlock(int id)
        => Ok(await _service.Unlock(id));
}
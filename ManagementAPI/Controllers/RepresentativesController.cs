using ManagementAPI.Dtos.Representative;
using ManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;

namespace ManagementAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RepresentativesController : ControllerBase
{
        private readonly IRepresentativeService _service;
    public RepresentativesController(IRepresentativeService service)
    {
        _service = service;
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRepresentativeRequestDto request)
        => Ok(await _service.Create(request));

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] FetchRepresentativeRequestDto filter)
        => Ok(await _service.GetAll(filter));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id) => Ok(await _service.GetById(Guid.Parse(id)));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id) => Ok(await _service.Delete(Guid.Parse(id)));

    [HttpGet("{id}/Download")]
    public async Task<IActionResult> Download(string id)
     => Ok(await _service.Download(Guid.Parse(id)));

    [HttpPut("{id}/lock")]
    public async Task<IActionResult> Lock(string id) => Ok(await _service.Lock(Guid.Parse(id)));

    [HttpPut("{id}/Unlock")]
    public async Task<IActionResult> Unlock(string id) 
        =>  Ok(await _service.Unlock(Guid.Parse(id)));
       
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateRepresentativeRequestDto request)
    => Ok(await _service.Update(Guid.Parse(id), request));

}

using ManagementAPI.Dtos.Representive;
using ManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;

namespace ManagementAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RepresentivesController : ControllerBase
{
        private readonly IRepresentiveService _service;
    public RepresentivesController(IRepresentiveService service)
    {
        _service = service;
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRepresentiveRequestDto request)
        => Ok(await _service.Create(request));

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] FetchRepresentiveRequestDto filter)
        => Ok(await _service.GetAll(filter));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
        => Ok( await _service.Delete(id));

    [HttpPut("{id}/lock")]
    public async Task<IActionResult> Lock(int id)
        =>Ok( await _service.Lock(id));

    [HttpPut("{id}/Unlock")]
    public async Task<IActionResult> Unlock(int id) 
        =>  Ok(await _service.Unlock(id));
       
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateRepresentiveRequestDto request)
    => Ok(await _service.Update(id, request));

}

using Infrastructure.Models;
using ManagementAPI.Dtos.Customer;
using ManagementAPI.Dtos.Representive;
using ManagementAPI.Services;
using Microsoft.AspNetCore.Http;
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
    {
        var result = await _service.Create(request);
        if (result.StatusCode == System.Net.HttpStatusCode.OK)
            return Ok(result.Msg);
        return BadRequest(result.Msg);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] FetchRepresentivesRequestDto filter)
    {
     var result= await _service.GetAll(filter);
            return Ok(result);
       
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.Delete(id);
        if(result.StatusCode == System.Net.HttpStatusCode.OK)
            return Ok(result.Msg);
        return BadRequest(result.Msg);
    }

    [HttpPut("{id}/lock")]
    public async Task<IActionResult> Lock(int id)
    {
        var result = await _service.Lock(id);
        if (result.StatusCode == System.Net.HttpStatusCode.OK)
            return Ok(result.Msg);
        return BadRequest(result.Msg);
    }
    [HttpPut("{id}/Unlock")]
    public async Task<IActionResult> Unlock(int id)
    {
        var result = await _service.Unlock(id);
        if (result.StatusCode == System.Net.HttpStatusCode.OK)
            return Ok(result.Msg);
        return BadRequest(result.Msg);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateRepresentiveRequestDto request)
    {
        var result = await _service.Update(id, request);
        if (result.StatusCode == System.Net.HttpStatusCode.OK)
            return Ok(result.Msg);
        return BadRequest(result.Msg);
    }
}

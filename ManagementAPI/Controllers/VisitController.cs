using Infrastructure.Models;
using ManagementAPI.Dtos.Companion;
using ManagementAPI.Dtos.Customer;
using ManagementAPI.Dtos.Service;
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


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateVisitRequestDto request)
         => Ok(await _service.Create(request));

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] FetchVisitRequestDto filter)
        => Ok(await _service.GetAll(filter));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
      => Ok(await _service.GetById(Guid.Parse(id)));
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, UpdateVisitRequestDto request)
        => Ok(await _service.Update(Guid.Parse(id), request));

    [HttpPut("{id}/lock")]
    public async Task<IActionResult> Lock(string id) 
        => Ok(await _service.Lock(Guid.Parse(id)));
    [HttpPut("{id}/Unlock")]
    public async Task<IActionResult> Unlock(string id) 
        => Ok(await _service.Unlock(Guid.Parse(id)));   
    [HttpPut("{id}/Pay")]
    public async Task<IActionResult> Paid(string id, Guid invioceId) 
        => Ok(await _service.Paid(Guid.Parse(id), invioceId));
}

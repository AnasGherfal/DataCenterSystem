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
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateVisitRequestDto request)
        => Ok(await _service.Update(id, request));

    [HttpPut("{id}/lock")]
    public async Task<IActionResult> Lock(int id) 
        => Ok(await _service.Lock(id));
    [HttpPut("{id}/Unlock")]
    public async Task<IActionResult> Unlock(int id) 
        => Ok(await _service.Unlock(id));
    [HttpPut("{id}/Pay")]
    public async Task<IActionResult> Paid(int id, int invioceId) 
        => Ok(await _service.Paid(id, invioceId));
}

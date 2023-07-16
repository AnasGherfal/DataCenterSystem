using Infrastructure;
using ManagementAPI.Dtos.Customer;
using ManagementAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using ManagementAPI.Dtos.Service;
using Azure;
using Azure.Core;
using System.Net;

namespace ManagementAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ServiceController : ControllerBase
{
    private readonly ServiceServices _service;
    public ServiceController(ServiceServices service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateServiceDto request)
         => Ok(await _service.Create(request));
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
       => Ok(await _service.GetById(id));
    [HttpGet]

    public async Task<IActionResult> GetAll([FromQuery] FetchServicesRequestDto filter)
        => Ok(await _service.GetAll(filter));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateServiceDto request)
        => Ok(await _service.Update(id, request));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
        => Ok(await _service.Delete(id));

    [HttpPut("{id}/lock")]
    public async Task<IActionResult> Lock(Guid id)
        => Ok(await _service.Lock(id));

    [HttpPut("{id}/unlock")]
    public async Task<IActionResult> Unlock(Guid id)
        => Ok(await _service.Unlock(id));
}


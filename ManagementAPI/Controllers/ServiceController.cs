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
    private readonly IServiceServices _service;
    public ServiceController(IServiceServices service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateServiceDto request)
         => Ok(await _service.Create(request));
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id) => Ok(await _service.GetById(Guid.Parse(id)));
    [HttpGet]

    public async Task<IActionResult> GetAll([FromQuery] FetchServicesRequestDto filter)
        => Ok(await _service.GetAll(filter));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateServiceDto request)
        => Ok(await _service.Update(Guid.Parse(id), request));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id) => Ok(await _service.Delete(Guid.Parse(id)));

    [HttpPut("{id}/lock")]
    public async Task<IActionResult> Lock(string id) => Ok(await _service.Lock(Guid.Parse(id)));

    [HttpPut("{id}/unlock")]
    public async Task<IActionResult> Unlock(string id) => Ok(await _service.Unlock(Guid.Parse(id)));
}


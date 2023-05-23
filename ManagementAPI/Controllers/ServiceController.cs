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
using ManagementAPI.Dtos.Subscriptions;

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

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] FetchServicesRequestDto request)
              => Ok(await _service.GetAll(request));

    [HttpGet("{id:int}/GetById")]
    public async Task<IActionResult> GetById(int id)
             => Ok(await _service.GetById(id));
    [HttpGet("{id:int}/Update")]
    public async Task<IActionResult> Update(int id,UpdateServiceDto request)
             => Ok(await _service.Update(id,request));

    [HttpDelete("{id:int}/Delete")]
    public async Task<IActionResult> Remove(int id)
             => Ok(await _service.Remove(id));
    [HttpPut("{id:int}/lock")]
    public async Task<IActionResult> Lock(int id)
             => Ok(await _service.Lock(id));
    [HttpPut("{id:int}/unlock")]
    public async Task<IActionResult> Unlock(int id)
             => Ok(await _service.Unlock(id));
}

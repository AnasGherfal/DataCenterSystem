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
    public async Task<OperationResponse> Create([FromBody]CreateServiceDto request)
    {
        return await _service.Create(request);
    }
    
    [HttpGet]
    public async Task<ActionResult<FetchServicesResponseDto>> GetAll([FromQuery]FetchServicesRequestDto fetchServicesRequestDto)
    {
        return Ok(await _service.GetAll(fetchServicesRequestDto));
    }
    [HttpPut]
    public async Task<OperationResponse> Update(int id, UpdateServiceDto updateServiceDto)
    {
        return await _service.Update(id,updateServiceDto);
    }

    [HttpDelete]
    public async Task<OperationResponse> Remove(int id)
    {

        return await _service.Remove(id);

    }
    [HttpPut("{id}/lock")]
    public async Task<OperationResponse> Lock(int id)
    {
        return await _service.Lock(id);
    }
    [HttpPut("{id}/unlock")]
    public async Task<OperationResponse> UnLock(int id)
    {
        return await _service.Unlock(id);
    }
}

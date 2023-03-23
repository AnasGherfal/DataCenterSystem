using Infrastructure;
using ManagementAPI.Dtos.Customer;
using ManagementAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using ManagementAPI.Dtos.Service;
using Azure;

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
    public async Task<OperationResponse> CreateService([FromBody]CreateServiceDto request)
    {
        return await _service.CreateService(request);


    }
    
    [HttpGet]
    public async Task<ActionResult> GetService([FromQuery]FetchServicesRequestDto fetchServicesRequestDto)
    {
        var get = await _service.GetAllService(fetchServicesRequestDto.PageNumber, fetchServicesRequestDto.PageSize);
        return Ok(get);

    }
}

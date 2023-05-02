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
    public async Task<OperationResponse> Create([FromBody] CreateRepresentiveRequestDto request)
    {
        return await _service.Create(request);
    }

    [HttpGet]
    public async Task<FetchRepresentiveResponseDto> GetAll([FromQuery] FetchRepresentiveRequestDto filter)
    {
        return await _service.GetAll(filter);
    }

    [HttpDelete("{id}")]
    public async Task<OperationResponse> Delete(int id)
    {
       return await _service.Delete(id);
    }

    [HttpPut("{id}/lock")]
    public async Task<OperationResponse> Lock(int id)
    {
        return await _service.Lock(id);
    }
    [HttpPut("{id}/Unlock")]
    public async Task<OperationResponse> Unlock(int id)
    {
        return await _service.Unlock(id);
       
    }

    [HttpPut("{id}")]
    public async Task<OperationResponse> Update(int id, [FromBody] UpdateRepresentiveRequestDto request)
    {
        return await _service.Update(id, request);
        
    }
}

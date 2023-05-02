using ManagementAPI.Dtos.Companion;
using ManagementAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;

namespace ManagementAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompanionsController : ControllerBase
{
    private readonly ICompanionService _service;
    public CompanionsController(ICompanionService service)
    {
        _service = service;  
    }
    [HttpPost]
    public async Task<OperationResponse> Create([FromBody] CreateCompanionRequestDto request)
    {
        return await _service.Create(request);
    }

    [HttpGet]
    public async Task<FetchCompanionResponseDto> GetAll([FromQuery] FetchCompanionRequestDto filter)
    {
        return await _service.GetAll(filter);
    }
    [HttpPut("{id}")]
    public async Task<OperationResponse> Update(int id, [FromBody] UpdateCompanionRequestDto request)
    {
        return await _service.Update(id, request);
    }
    [HttpDelete("{id}")]
    public async Task<OperationResponse> Delete(int id)
    {
        return await _service.Delete(id);
    }
}

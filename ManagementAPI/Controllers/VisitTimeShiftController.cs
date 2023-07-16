using ManagementAPI.Dtos.Customer;
using ManagementAPI.Dtos.VisitTimeShift;
using ManagementAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;

namespace ManagementAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VisitTimeShiftController : ControllerBase
{
    private readonly IVisitTimeShiftService _service;
    public VisitTimeShiftController(IVisitTimeShiftService service)
    {
        _service = service;
    }
    [HttpPost]
    public async Task<OperationResponse> Create([FromBody] CreateVisitTimeShiftRequestDto request)
    {
        return await _service.Create(request);
    }

    [HttpGet]
    public async Task<FetchVisitTimeShiftResponseDto> GetAll([FromQuery] FetchVisitTimeShiftRequestDto filter)
    {
        return await _service.GetAll(filter);
    }
    [HttpPut("{id}")]
    public async Task<OperationResponse> Update(Guid id, [FromBody] UpdateVisitTimeShiftRequestDto request)
    {
        return await _service.Update(id, request);
    }
    [HttpDelete("{id}")]
    public async Task<OperationResponse> Delete(Guid id)
    {
        return await _service.Delete(id);
    }

    [HttpPut("{id}/lock")]
    public async Task<OperationResponse> Lock(Guid id)
    {
        return await _service.Lock(id);
    }
    [HttpPut("{id}/unlock")]
    public async Task<OperationResponse> Unlock(Guid id)
    {
        return await _service.Unlock(id);
    }
}


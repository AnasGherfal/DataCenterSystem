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
    public async Task<OperationResponse> Update(string id, [FromBody] UpdateVisitTimeShiftRequestDto request)
    {
        return await _service.Update(Guid.Parse(id), request);
    }
    [HttpDelete("{id}")]
    public async Task<OperationResponse> Delete(string id)
    {
        return await _service.Delete(Guid.Parse(id));
    }

    [HttpPut("{id}/lock")]
    public async Task<OperationResponse> Lock(string id)
    {
        return await _service.Lock(Guid.Parse(id));
    }
    [HttpPut("{id}/unlock")]
    public async Task<OperationResponse> Unlock(string id)
    {
        return await _service.Unlock(Guid.Parse(id));
    }
}


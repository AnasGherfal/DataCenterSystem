using ManagementAPI.Dtos.Customer;
using ManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Constants;

namespace ManagementAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _service;
    public CustomersController(ICustomerService service)
    {
        _service = service;
    }

     [HttpPost]
     public async Task<IActionResult> Create([FromForm] CreateCustomerRequestDto request)
         => Ok(await _service.Create(request));
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
       => Ok(await _service.GetById(Guid.Parse(id)));

    [HttpGet("{id}/Download")]
    public async Task<IActionResult> Download(string id)
     => Ok(await _service.Download(Guid.Parse(id)));
    [HttpGet]
    
    public async Task<IActionResult> GetAll([FromQuery] FetchCustomersRequestDto filter)
        => Ok(await _service.GetAll(filter));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromForm] UpdateCustomerRequestDto request)
        => Ok(await _service.Update(Guid.Parse(id), request));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
        => Ok(await _service.Delete(Guid.Parse(id)));

    [HttpPut("{id}/lock")]
    public async Task<IActionResult> Lock(string id)
        => Ok(await _service.Lock(Guid.Parse(id)));
    
    [HttpPut("{id}/unlock")]
    public async Task<IActionResult> Unlock(string id)
        => Ok(await _service.Unlock(Guid.Parse(id)));
}

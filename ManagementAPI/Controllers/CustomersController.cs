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
    public async Task<IActionResult> GetById(int id)
       => Ok(await _service.GetById(id));
    [HttpGet]
    
    public async Task<IActionResult> GetAll([FromQuery] FetchCustomersRequestDto filter)
        => Ok(await _service.GetAll(filter));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCustomerRequestDto request)
        => Ok(await _service.Update(id, request));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
        => Ok(await _service.Delete(id));

    [HttpPut("{id}/lock")]
    public async Task<IActionResult> Lock(int id)
        => Ok(await _service.Lock(id));
    
    [HttpPut("{id}/unlock")]
    public async Task<IActionResult> Unlock(int id) 
        => Ok(await _service.Unlock(id));
}

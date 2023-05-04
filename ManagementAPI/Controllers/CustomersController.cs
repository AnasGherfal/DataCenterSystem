using ManagementAPI.Dtos.Customer;
using ManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ManagementAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _service;
    //TODO: REVIEW [Warning]: Remove unused variable
    private readonly ICustomerFileService _fileService;
    public CustomersController(ICustomerService service, ICustomerFileService fileService)
    {
        _service = service;
        _fileService = fileService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerRequestDto request)
        => Ok(await _service.Create(request));

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

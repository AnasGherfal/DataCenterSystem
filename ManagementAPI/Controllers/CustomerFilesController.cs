using ManagementAPI.Dtos.Customer;
using ManagementAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;

namespace ManagementAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerFilesController : ControllerBase
{
    private readonly ICustomerFileService _service;
    public CustomerFilesController(ICustomerFileService service)
    {
        _service = service;
    }
    [HttpPost]
    public async Task<OperationResponse> Upload([FromForm]CustomerFileRequestDto request)
    {
        return await _service.Upload(request);
    }
}

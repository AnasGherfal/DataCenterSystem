using Infrastructure.Models;
using ManagementAPI.Dtos.Customer;
using ManagementAPI.Dtos.Invoice;
using ManagementAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManagementAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvoicesController : ControllerBase
{
    private readonly IInvoiceService _service;
    public InvoicesController(IInvoiceService service)
    {
        _service = service;
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateInvoiceRequestDto request)
         => Ok(await _service.Create(request));
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery]FetchInvoicesRequestDto request)
        => Ok(await _service.GetAll(request));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
        => Ok(await _service.GetById(id));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateInvoiceRequestDto request)
        => Ok(await _service.Update(id, request));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
        => Ok(await _service.Delete(id));

    [HttpPut("{id}/lock")]
    public async Task<IActionResult> Lock(Guid id)
        => Ok(await _service.Lock(id));

    [HttpPut("{id}/unlock")]
    public async Task<IActionResult> Unlock(Guid id)
        => Ok(await _service.Unlock(id));
}

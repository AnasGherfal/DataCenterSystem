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
    public async Task<IActionResult> GetById(string id)
        => Ok(await _service.GetById(Guid.Parse(id)));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateInvoiceRequestDto request)
        => Ok(await _service.Update(Guid.Parse(id), request));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
        => Ok(await _service.Delete(Guid.Parse(id)));

    [HttpPut("{id}/lock")]
    public async Task<IActionResult> Lock(string id) => Ok(await _service.Lock(Guid.Parse(id)));

    [HttpPut("{id}/unlock")]
    public async Task<IActionResult> Unlock(string id) => Ok(await _service.Unlock(Guid.Parse(id)));
}

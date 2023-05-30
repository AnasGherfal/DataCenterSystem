using Infrastructure.Models;
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
    public async Task<IActionResult> GetById(int id)
        => Ok(_service.GetById(id));

}

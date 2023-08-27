using Core.Constants;
using Core.Dtos;
using Microsoft.AspNetCore.Mvc;
using Web.API.Abstracts;
using Web.API.Features.InvoiceManagement.CreateInvoice;
using Web.API.Features.InvoiceManagement.FetchInvoiceById;
using Web.API.Features.InvoiceManagement.FetchInvoices;
using Web.API.Features.InvoiceManagement.MarkInvoiceAsPaid;
using Web.API.Filters;

namespace Web.API.Controllers.Management;

[ApiController]
public class InvoicesController : ManagementController
{
    [HttpPost]
    public async Task<MessageResponse> Create([FromForm] CreateInvoiceCommand request) 
        => await Mediator.Send(request);
    
    [VerifiedAdmin(SystemPermissions.InvoiceManagement)]
    [HttpGet]
    public async Task<PagedResponse<FetchInvoicesQueryResponse>> Fetch([FromQuery] FetchInvoicesQuery request) 
        => await Mediator.Send(request);
    
    [HttpGet("{id}")]
    public async Task<ContentResponse<FetchInvoiceByIdQueryResponse>> FetchById(string id)
        => await Mediator.Send(new FetchInvoiceByIdQuery()
        {
            Id = id,
        });


    [HttpPut("{id}/paid")]
    public async Task<MessageResponse> Renew(string id, [FromBody] MarkInvoiceAsPaidCommand request)
    {
        request.SetId(id);
        return await Mediator.Send(request);
    }
}
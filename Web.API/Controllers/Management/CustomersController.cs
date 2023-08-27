using Common.Constants;
using Infrastructure.Constants;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Web.API.Abstracts;
using Web.API.Features.CustomerManagement.CreateCustomer;
using Web.API.Features.CustomerManagement.DeleteCustomer;
using Web.API.Features.CustomerManagement.FetchCustomerById;
using Web.API.Features.CustomerManagement.FetchCustomerFileById;
using Web.API.Features.CustomerManagement.FetchCustomers;
using Web.API.Features.CustomerManagement.LockCustomer;
using Web.API.Features.CustomerManagement.UnlockCustomer;
using Web.API.Features.CustomerManagement.UpdateCustomer;
using Web.API.Features.CustomerManagement.UpdateCustomerFile;
using Web.API.Features.RepresentativeManagement.CreateRepresentative;
using Web.API.Features.RepresentativeManagement.DeleteRepresentative;
using Web.API.Features.RepresentativeManagement.FetchRepresentativeById;
using Web.API.Features.RepresentativeManagement.FetchRepresentativeFileById;
using Web.API.Features.RepresentativeManagement.FetchRepresentatives;
using Web.API.Features.RepresentativeManagement.LockRepresentative;
using Web.API.Features.RepresentativeManagement.UnlockRepresentative;
using Web.API.Features.RepresentativeManagement.UpdateRepresentative;
using Web.API.Filters;

namespace Web.API.Controllers.Management;

[VerifiedAdmin(SystemPermissions.CustomerManagement)]
[ApiController]
public class CustomersController : ManagementController
{
    [HttpPost]
    public async Task<MessageResponse> Create([FromForm] CreateCustomerCommand request) 
        => await Mediator.Send(request);
    
    [HttpGet]
    public async Task<PagedResponse<FetchCustomersQueryResponse>> Fetch([FromQuery] FetchCustomersQuery request) 
        => await Mediator.Send(request);
    
    [HttpGet("{id}")]
    public async Task<ContentResponse<FetchCustomerByIdQueryResponse>> FetchById(string id)
        => await Mediator.Send(new FetchCustomerByIdQuery()
        {
            Id = id,
        });
    
    [HttpGet("{id}/document/{fileId}")]
    public async Task<FileStreamResult> FetchById(string id, string fileId)
        => await Mediator.Send(new FetchCustomerFileByIdQuery()
        {
            CustomerId = id,
            FileId = fileId,
        });

    [HttpPost("{id}/document/")]
    public async Task<MessageResponse> UpdateFile(string id, [FromForm] UpdateCustomerFileCommand request)
    {
        request.SetCustomerId(id);
        return await Mediator.Send(request);
    }
    
    [HttpPut("{id}/document/{fileId}")]
    public async Task<MessageResponse> UpdateFile(string id, string fileId, [FromForm] UpdateCustomerFileCommand request)
    {
        request.SetCustomerId(id);
        request.SetFileId(fileId);
        return await Mediator.Send(request);
    }

    [HttpPut("{id}")]
    public async Task<MessageResponse> Update(string id, [FromBody] UpdateCustomerCommand request)
    {
        request.SetId(id);
        return await Mediator.Send(request);
    }

    [HttpPut("{id}/lock")]
    public async Task<MessageResponse> Lock(string id) 
        => await Mediator.Send(new LockCustomerCommand()
        {
            Id = id,
        });
    
    [HttpPut("{id}/unlock")]
    public async Task<MessageResponse> UnLock(string id) 
        => await Mediator.Send(new UnlockCustomerCommand()
        {
            Id = id,
        });
    
    [HttpDelete("{id}")]
    public async Task<MessageResponse> Delete(string id) 
        => await Mediator.Send(new DeleteCustomerCommand()
        {
            Id = id,
        });
}
using Core.Constants;
using Core.Wrappers;
using MediatR;

namespace Web.API.Features.CustomerManagement.CreateCustomer;

public sealed record CreateCustomerCommand : IRequest<MessageResponse>
{
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? PrimaryPhone { get; set; }
    public string? SecondaryPhone { get; set; }
    public string? Email { get; set; }
    public IList<CreateCustomerCommandItem>? Documents { get; set; }
}

public sealed record CreateCustomerCommandItem
{
    public IFormFile? File { get; private set; }
    public DocumentType? DocType { get; private set; }
}

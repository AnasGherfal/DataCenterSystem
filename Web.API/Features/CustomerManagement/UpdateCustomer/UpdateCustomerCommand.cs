using Core.Constants;
using Core.Wrappers;
using MediatR;

namespace Web.API.Features.CustomerManagement.UpdateCustomer;

public sealed record UpdateCustomerCommand : IRequest<MessageResponse>
{
    public string? Id { get; private set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PrimaryPhone { get; set; }
    public string? SecondaryPhone { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public void SetId(string id) => Id = id;
}
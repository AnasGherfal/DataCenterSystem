using Core.Constants;
using Core.Dtos;
using MediatR;

namespace Web.API.Features.CustomerManagement.UpdateCustomer;

public sealed record UpdateCustomerCommand : IRequest<MessageResponse>
{
    public string? Id { get; private set; }
    public string? IdentityNo { get; set; }
    public IdentityType? IdentityType { get; set; } 
    public string? Email { get; set; }
    public string? PhoneNo { get; set; }
    public void SetId(string id) => Id = id;
}
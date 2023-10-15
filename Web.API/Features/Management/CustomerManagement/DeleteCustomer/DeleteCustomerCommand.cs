using Core.Wrappers;
using MediatR;

namespace Web.API.Features.CustomerManagement.DeleteCustomer;

public sealed record DeleteCustomerCommand : IRequest<MessageResponse>
{
    public string? Id { get; set; }
}
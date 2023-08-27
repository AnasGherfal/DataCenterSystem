using MediatR;
using Shared.Dtos;

namespace Web.API.Features.CustomerManagement.DeleteCustomer;

public sealed record DeleteCustomerCommand : IRequest<MessageResponse>
{
    public string? Id { get; set; }
}
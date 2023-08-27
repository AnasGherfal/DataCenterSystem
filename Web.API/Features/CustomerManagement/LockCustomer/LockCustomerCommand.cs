using MediatR;
using Shared.Dtos;

namespace Web.API.Features.CustomerManagement.LockCustomer;

public sealed record LockCustomerCommand : IRequest<MessageResponse>
{
    public string? Id { get; set; }
}
using MediatR;
using Shared.Dtos;

namespace Web.API.Features.CustomerManagement.UnlockCustomer;

public sealed record UnlockCustomerCommand : IRequest<MessageResponse>
{
    public string? Id { get; set; }
}
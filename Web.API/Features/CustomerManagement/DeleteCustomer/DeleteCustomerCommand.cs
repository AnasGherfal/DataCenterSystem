using Core.Dtos;
using MediatR;

namespace Web.API.Features.CustomerManagement.DeleteCustomer;

public sealed record DeleteCustomerCommand : IRequest<MessageResponse>
{
    public string? Id { get; set; }
}
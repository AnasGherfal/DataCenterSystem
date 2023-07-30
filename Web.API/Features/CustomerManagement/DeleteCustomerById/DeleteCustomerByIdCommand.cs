using MediatR;
using Shared.Dtos;

namespace Web.API.Features.CustomerManagement.DeleteCustomerById;

public sealed record DeleteCustomerByIdCommand(string? Id)
    : IRequest<MessageResponse>;
 

using MediatR;
using Shared.Dtos;

namespace Web.API.Features.CustomerManagement.CreateCustomer;

public sealed record CreateCustomerCommand(string? Name, string? Address, string? PrimaryPhone, string? SecondaryPhone, string? Email, IList<FileRequestDto>? Files)
: IRequest<MessageResponse>;

using MediatR;
using Shared.Dtos;

namespace Web.API.Features.AdminsManagement.LockAdminById;

public sealed record LockAdminByIdCommand(string? Id) 
    : IRequest<MessageResponse>;
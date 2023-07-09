using MediatR;
using Shared.Dtos;

namespace Web.API.Features.AdminsManagement.UnlockAdminById;

public sealed record UnlockAdminByIdCommand(string? Id) 
    : IRequest<MessageResponse>;
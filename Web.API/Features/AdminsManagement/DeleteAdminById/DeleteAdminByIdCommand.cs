using MediatR;
using Shared.Dtos;

namespace Web.API.Features.AdminsManagement.DeleteAdminById;

public sealed record DeleteAdminByIdCommand(string? Id) 
    : IRequest<MessageResponse>;
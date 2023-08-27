using Core.Dtos;
using MediatR;

namespace Web.API.Features.AdminsManagement.LockAdminById;

public sealed record LockAdminByIdCommand: IRequest<MessageResponse>
{
    public string? Id { get; set; }
}
using Core.Wrappers;
using MediatR;

namespace Web.API.Features.Management.AdminsManagement.LockAdminById;

public sealed record LockAdminByIdCommand: IRequest<MessageResponse>
{
    public string? Id { get; set; }
}
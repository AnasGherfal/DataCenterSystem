using Core.Wrappers;
using MediatR;

namespace Web.API.Features.AdminsManagement.UnlockAdminById;

public sealed record UnlockAdminByIdCommand: IRequest<MessageResponse>
{
    public string? Id { get; set; }
}
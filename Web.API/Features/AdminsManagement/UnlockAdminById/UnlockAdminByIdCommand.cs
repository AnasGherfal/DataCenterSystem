using MediatR;
using Shared.Dtos;

namespace Web.API.Features.AdminsManagement.UnlockAdminById;

public sealed record UnlockAdminByIdCommand: IRequest<MessageResponse>
{
    public string? Id { get; set; }
}
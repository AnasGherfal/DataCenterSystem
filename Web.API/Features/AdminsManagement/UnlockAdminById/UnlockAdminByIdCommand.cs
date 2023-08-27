using Core.Dtos;
using MediatR;

namespace Web.API.Features.AdminsManagement.UnlockAdminById;

public sealed record UnlockAdminByIdCommand: IRequest<MessageResponse>
{
    public string? Id { get; set; }
}
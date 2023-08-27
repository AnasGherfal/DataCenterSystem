using Core.Wrappers;
using MediatR;

namespace Web.API.Features.AdminsManagement.DeleteAdminById;

public sealed record DeleteAdminByIdCommand: IRequest<MessageResponse>
{
    public string? Id { get; set; }
}
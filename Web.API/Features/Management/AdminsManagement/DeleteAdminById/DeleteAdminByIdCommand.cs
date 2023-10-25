using Core.Wrappers;
using MediatR;

namespace Web.API.Features.Management.AdminsManagement.DeleteAdminById;

public sealed record DeleteAdminByIdCommand: IRequest<MessageResponse>
{
    public string? Id { get; set; }
}
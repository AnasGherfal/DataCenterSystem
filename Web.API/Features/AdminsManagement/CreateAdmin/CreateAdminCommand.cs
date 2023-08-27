using Core.Constants;
using Core.Wrappers;
using MediatR;

namespace Web.API.Features.AdminsManagement.CreateAdmin;

public sealed record CreateAdminCommand: IRequest<ContentResponse<CreateAdminCommandResponse>>
{
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public SystemPermissions? Permissions { get; set; }
    public int? EmpId { get; set; }
}
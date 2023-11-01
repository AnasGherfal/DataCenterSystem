using Core.Wrappers;
using MediatR;

namespace Web.API.Features.Management.AdminsManagement.ResetAdminPasswordById;

public sealed record ResetAdminPasswordByIdCommand: IRequest<ContentResponse<string>>
{
    public string? Id { get; set; }
}
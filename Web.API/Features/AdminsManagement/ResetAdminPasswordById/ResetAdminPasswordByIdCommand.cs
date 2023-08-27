using Core.Dtos;
using MediatR;

namespace Web.API.Features.AdminsManagement.ResetAdminPasswordById;

public sealed record ResetAdminPasswordByIdCommand: IRequest<ContentResponse<string>>
{
    public string? Id { get; set; }
}
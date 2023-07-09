using MediatR;
using Shared.Dtos;

namespace Web.API.Features.AdminsManagement.ResetAdminPasswordById;

public sealed record ResetAdminPasswordByIdCommand(string? Id) 
    : IRequest<ContentResponse<string>>;
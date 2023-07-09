using MediatR;
using Shared.Dtos;

namespace Web.API.Features.AdminsManagement.CreateAdmin;

public sealed record CreateAdminCommand(string? FullName, string? Email, long? Permissions, int? EmpId) 
    : IRequest<ContentResponse<CreateAdminCommandResponse>>;
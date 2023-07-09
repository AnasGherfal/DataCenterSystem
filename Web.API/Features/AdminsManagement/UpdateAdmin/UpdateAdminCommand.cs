using Common.Constants;
using MediatR;
using Shared.Dtos;

namespace Web.API.Features.AdminsManagement.UpdateAdmin;

public sealed record UpdateAdminCommand : IRequest<MessageResponse>
{
    public string? Id { get; private set; } = string.Empty;
    public SystemPermissions? Permissions { get; set; }
    
    public void SetId(string id)
    {
        Id = id;
    }
}
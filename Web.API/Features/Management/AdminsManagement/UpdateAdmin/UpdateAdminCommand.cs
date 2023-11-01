using Core.Constants;
using Core.Wrappers;
using MediatR;

namespace Web.API.Features.Management.AdminsManagement.UpdateAdmin;

public sealed record UpdateAdminCommand : IRequest<MessageResponse>
{
    public string? Id { get; private set; } = string.Empty;
    public SystemPermissions? Permissions { get; set; }
    
    public void SetId(string id)
    {
        Id = id;
    }
}
namespace Web.API.Features.Management.AdminsManagement.CreateAdmin;

public sealed record CreateAdminCommandResponse
{
    public string UserId { get; set; } = string.Empty;
    public string UserPassword { get; set; } = string.Empty;
}
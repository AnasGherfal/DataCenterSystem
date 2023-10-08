using Core.Constants;
using Core.Exceptions;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.ClientInfo;

public class ClientService: IClientService
{
    public string IdentityId { get; }
    public string DisplayName { get; }
    public string Email { get; }
    public bool EmailVerified { get; }
    public long Permission { get; }
    public Guid GetIdentifier()
    {
        try
        {
            return Guid.Empty;
            // return Guid.Parse(IdentityId);
        }
        catch
        {
            throw new FlowException("", "FlowExceptionIdentityIdNotFound");
        }
    }
    
    public ClientService(IHttpContextAccessor httpContextAccessor)
    {
        // IdentityId = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimsKey.IdentityId.Key())?.Value ?? "";
        IdentityId = Guid.Empty.ToString();
        // DisplayName = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimsKey.DisplayName.Key())?.Value ?? "User";
        DisplayName = "User";
        // Email = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimsKey.Email.Key())?.Value ?? "";
        Email = "admin@ltt.ly";
        // Permission = long.Parse(httpContextAccessor.HttpContext?.User?.FindFirst(ClaimsKey.Permissions.Key())?.Value ?? "0");
        Permission = (long) SystemPermissions.SuperAdmin;
        // EmailVerified = bool.Parse(httpContextAccessor.HttpContext?.User?.FindFirst(ClaimsKey.EmailVerified.Key())?.Value ?? "False");
        EmailVerified = true;
    }
}
using Core.Constants;
using Core.Exceptions;

namespace Web.API.Services.ClientService;

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
            return Guid.Parse(IdentityId);
        }
        catch
        {
            throw new FlowException("", "FlowExceptionIdentityIdNotFound");
        }
    }
    
    public ClientService(IHttpContextAccessor httpContextAccessor)
    {
        IdentityId = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimsKey.IdentityId.Key())?.Value ?? "";
        DisplayName = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimsKey.DisplayName.Key())?.Value ?? "User";
        Email = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimsKey.Email.Key())?.Value ?? "";
        Permission = long.Parse(httpContextAccessor.HttpContext?.User?.FindFirst(ClaimsKey.Permissions.Key())?.Value ?? "0");
        EmailVerified = bool.Parse(httpContextAccessor.HttpContext?.User?.FindFirst(ClaimsKey.EmailVerified.Key())?.Value ?? "False");
    }
}
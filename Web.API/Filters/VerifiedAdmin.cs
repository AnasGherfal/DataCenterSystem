using Core.Constants;
using Core.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.API.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class VerifiedAdmin : Attribute, IAuthorizationFilter
{
    private readonly SystemPermissions _requiredPermission;
    
    public VerifiedAdmin(SystemPermissions permission = SystemPermissions.None)
    {
        _requiredPermission = permission;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!HasIdentifier(context)) throw new NotAuthenticatedException("Not Authenticated");
        if (!HasPermission(context)) throw new ForbiddenException("Permission Denied");
    }
    
    private static bool HasIdentifier(AuthorizationFilterContext context)
    {
        var identifier = context.HttpContext.User.FindFirst(ClaimsKey.IdentityId.Key())?.Value ?? "";
        return !string.IsNullOrWhiteSpace(identifier);
    }
    
    private bool HasPermission(AuthorizationFilterContext context)
    {
        var permission = (SystemPermissions) long.Parse((context.HttpContext.User.FindFirst(ClaimsKey.Permissions.Key())?.Value ?? "0"));
        return permission.HasFlag(_requiredPermission);
    }
}
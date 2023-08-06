using System.Security.Claims;
using Common.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.API.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class RoleBasedPermissionAttribute : Attribute, IAuthorizationFilter
{
    private readonly SystemPermissions _allowedRoles;

    public RoleBasedPermissionAttribute(SystemPermissions allowedRoles)
    {
        _allowedRoles = allowedRoles;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var roleClaim = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
        if (roleClaim != null && int.TryParse(roleClaim.Value, out int userRole))
        {
            if ((_allowedRoles & (SystemPermissions) userRole) != 0) return;
        }
        context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
    }
}
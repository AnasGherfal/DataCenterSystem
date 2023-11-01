using Core.Constants;
using Core.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.API.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class VerifiedCustomer : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!HasIdentifier(context)) throw new NotAuthenticatedException("Not Authenticated");
        if (!IsCustomer(context)) throw new ForbiddenException("Permission Denied");
    }
    
    private static bool HasIdentifier(AuthorizationFilterContext context)
    {
        var identifier = context.HttpContext.User.FindFirst(ClaimsKey.IdentityId.Key())?.Value ?? "";
        return !string.IsNullOrWhiteSpace(identifier);
    }
    
    private bool IsCustomer(AuthorizationFilterContext context)
    {
        var accountType = int.Parse((context.HttpContext.User.FindFirst(ClaimsKey.UserType.Key())?.Value ?? "-1"));
        return accountType == (int) AccountType.Customer;
    }
}
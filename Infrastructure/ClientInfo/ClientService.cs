﻿using Core.Constants;
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
    public int UserType { get; }
    public Guid GetIdentifier()
    {
        try
        {
            if (string.IsNullOrEmpty(IdentityId))
            {
                return Guid.Empty;
            }
            else
            {
                return Guid.Parse(IdentityId);
            }
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
        UserType = int.Parse(httpContextAccessor.HttpContext?.User?.FindFirst(ClaimsKey.UserType.Key())?.Value ?? "1");
        EmailVerified = bool.Parse(httpContextAccessor.HttpContext?.User?.FindFirst(ClaimsKey.EmailVerified.Key())?.Value ?? "False");
    }
}
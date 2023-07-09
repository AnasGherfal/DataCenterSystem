using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Infrastructure.Constants;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Web.API.Options;

namespace Web.API.Services.TokenService;

public class TokenService : ITokenService
{
    private readonly UserManager<Admin> _userManager;
    private readonly AuthenticationOption _option;

    public TokenService(UserManager<Admin> userManager, IOptions<AuthenticationOption> authenticationsOption)
    {
        _userManager = userManager;
        _option = authenticationsOption.Value;
    }
    
    public async Task<JwtSecurityToken> GenerateAccessToken(Admin admin)
    {
        var userClaims = await _userManager.GetClaimsAsync(admin);
        var authClaims = new List<Claim>
        {
            new(ClaimsKey.IdentityId.Key(), admin.Id.ToString()),
            new(ClaimsKey.DisplayName.Key(), admin.DisplayName),
            new(ClaimsKey.Permissions.Key(), admin.Permissions.ToString("D")),
            new(ClaimsKey.EmailVerified.Key(), admin.EmailConfirmed.ToString()),
        };
        authClaims.AddRange(userClaims);
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_option.Secret));
        var token = new JwtSecurityToken(
            _option.ValidIssuer,
            audience: _option.ValidAudience,
            expires: DateTime.UtcNow.AddDays(_option.TokenValidityInSecond),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );
        return token;
    }
}
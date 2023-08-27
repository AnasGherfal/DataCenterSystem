using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Core.Entities;
using Core.Interfaces.Services;
using Core.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Identity;

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
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_option.Secret));
        var token = new JwtSecurityToken(
            _option.ValidIssuer,
            audience: _option.ValidAudience,
            expires: DateTime.UtcNow.AddDays(_option.TokenValidityInSecond),
            claims: userClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );
        return token;
    }
}
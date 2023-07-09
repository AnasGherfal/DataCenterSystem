using System.IdentityModel.Tokens.Jwt;
using Infrastructure.Models;

namespace Web.API.Services.TokenService;

public interface ITokenService
{
    Task<JwtSecurityToken> GenerateAccessToken(Admin admin);
}
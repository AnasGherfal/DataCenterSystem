using System.IdentityModel.Tokens.Jwt;
using Core.Entities;

namespace Web.API.Services.TokenService;

public interface ITokenService
{
    Task<JwtSecurityToken> GenerateAccessToken(Admin admin);
}
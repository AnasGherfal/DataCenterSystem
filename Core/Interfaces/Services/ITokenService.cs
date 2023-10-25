using System.IdentityModel.Tokens.Jwt;
using Core.Entities;

namespace Core.Interfaces.Services;

public interface ITokenService
{
    Task<JwtSecurityToken> GenerateAccessToken(Admin admin);
    Task<JwtSecurityToken> GenerateAccessToken(Customer customer);
}
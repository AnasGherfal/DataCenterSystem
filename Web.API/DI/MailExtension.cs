using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Text;
using Infrastructure;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Web.API.Options;
using Web.API.Services.ClientService;
using Web.API.Services.MailService;
using Web.API.Services.TokenService;

namespace Web.API.DI;

public static class MailExtension
{
    public static void AddMail(this IServiceCollection services, IConfigurationSection section)
    {
        services.Configure<MailOption>(section);
        services.AddScoped<IMailService, MailService>();
    }
}
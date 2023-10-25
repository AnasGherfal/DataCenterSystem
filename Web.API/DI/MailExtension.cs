using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Text;
using Core.Interfaces.Services;
using Core.Options;
using Infrastructure.Mail;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Web.API.DI;

public static class MailExtension
{
    public static void AddMail(this IServiceCollection services, IConfigurationSection section)
    {
        services.Configure<MailOption>(section);
        services.AddScoped<IMailService, MailService>();
    }
}
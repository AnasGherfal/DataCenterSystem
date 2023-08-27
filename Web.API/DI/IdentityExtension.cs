using System.Text;
using Core.Entities;
using Core.Interfaces.Services;
using Core.Options;
using Infrastructure.ClientInfo;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Web.API.DI;

public static class IdentityExtension
{
    public static void AddIdentity(this IServiceCollection services, IConfigurationSection section)
    {
        services.Configure<AuthenticationOption>(section);
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddHttpContextAccessor();
        var authOption = section.Get<AuthenticationOption>()!;
        services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = true;
                options.Authority = authOption.ValidIssuer;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = authOption.ValidIssuer,
                    ValidAudience = authOption.ValidAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOption.Secret))
                };
            });
        services.AddIdentity<Admin, AdminRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 8;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.Lockout.AllowedForNewUsers = true;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(authOption.LockoutTimeInMinute);
                    options.Lockout.MaxFailedAccessAttempts = authOption.LockoutCount;
                    options.SignIn.RequireConfirmedEmail = false;
                    options.User.RequireUniqueEmail = true;
                }
            )
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        services.AddAuthorization();
    }
}
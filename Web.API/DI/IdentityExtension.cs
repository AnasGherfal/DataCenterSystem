using System.Text;
using Infrastructure;
using Infrastructure.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Web.API.Options;
using Web.API.Services.TokenService;

namespace Web.API.DI;

public static class IdentityExtension
{
    public static void AddIdentity(this IServiceCollection services, IConfigurationSection section)
    {
        services.Configure<AuthenticationOption>(section);
        services.AddScoped<ITokenService, TokenService>();
        var authOption = services.BuildServiceProvider().GetRequiredService<IOptions<AuthenticationOption>>().Value;
        services.AddAuthentication(options => 
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.Events = new JwtBearerEvents()
                {
                    OnTokenValidated = _ => Task.CompletedTask,
                    OnMessageReceived = _ => Task.CompletedTask
                };
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = authOption.ValidAudience,
                    ValidIssuer = authOption.ValidIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOption.Secret)),
                    ClockSkew = TimeSpan.Zero,
                };
            });
        
        services.AddIdentity<Admin, AdminRole>(
                options =>
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
            // .AddErrorDescriber<MultiLanguageIdentityErrorDescriber>()
            .AddEntityFrameworkStores<DataCenterContext>()
            .AddDefaultTokenProviders();
    }
}
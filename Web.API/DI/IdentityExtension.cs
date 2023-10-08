using System.Security.Claims;
using System.Text;
using Core.Entities;
using Core.Exceptions;
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
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOption.Secret)),
                    ValidAudience = authOption.ValidAudience,
                    ValidIssuer = authOption.ValidIssuer,
                    ClockSkew = TimeSpan.Zero,
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context => throw new NotAuthenticatedException("FAILED"),
                    OnForbidden = context => throw new ForbiddenException("FORBIDDEN"),
                };
            });
        services.AddIdentityCore<Admin>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
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
                    options.ClaimsIdentity.EmailClaimType = ClaimTypes.Email;
                    options.ClaimsIdentity.RoleClaimType = ClaimTypes.Role;
                    options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier;
                    options.ClaimsIdentity.UserNameClaimType = ClaimTypes.Name;
                    options.ClaimsIdentity.SecurityStampClaimType = ClaimTypes.Sid;
                    options.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider;
                }
            )
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        services.AddAuthorization();
    }
}
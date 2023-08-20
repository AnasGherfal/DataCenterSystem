using System.Text;
using Infrastructure;
using Infrastructure.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Web.API.Options;
using Web.API.Services.TokenService;
using Web.API.Services.UploadService;

namespace Web.API.DI;

public static class StorageExtension
{
    public static void AddFileStorage(this IServiceCollection services, IConfigurationSection section)
    {
        services.Configure<UploadOption>(section);
        services.AddScoped<IUploadFileService, UploadFileService>();
    }
}
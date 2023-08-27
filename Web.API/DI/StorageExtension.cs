using Core.Interfaces.Services;
using Core.Options;
using Infrastructure.FileStorage;

namespace Web.API.DI;

public static class StorageExtension
{
    public static void AddFileStorage(this IServiceCollection services, IConfigurationSection section)
    {
        services.Configure<UploadOption>(section);
        services.AddScoped<IUploadFileService, UploadFileService>();
    }
}
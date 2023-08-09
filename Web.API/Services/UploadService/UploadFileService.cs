using Infrastructure.Constants;
using Microsoft.Extensions.Options;
using Web.API.Options;

namespace Web.API.Services.UploadService;

public class UploadFileService: IUploadFileService
{
    private readonly IWebHostEnvironment _env;
    private readonly UploadOption _settings;

    public UploadFileService(IWebHostEnvironment env, IOptions<UploadOption> settings)
    {
        _env = env;
        _settings = settings.Value;
    }

    public async Task<string> Upload(IFormFile file, StorageType type)
    {
        if (file.Length == 0) throw new Exception("File is empty");
        if (file.Length > _settings.MaximumFileSizeInMb) throw new Exception("File size exceeded.");
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), type.FolderName(), fileName);
        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);
        return filePath;
    }
}
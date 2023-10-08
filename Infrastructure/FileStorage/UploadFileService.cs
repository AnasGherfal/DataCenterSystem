using Core.Constants;
using Core.Interfaces.Dtos;
using Core.Interfaces.Services;
using Core.Options;
using MailKit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.FileStorage;

public class UploadFileService: IUploadFileService
{
    private readonly ILogger<UploadFileService> _logger;
    private readonly IWebHostEnvironment _env;
    private readonly UploadOption _settings;

    public UploadFileService(IWebHostEnvironment env, IOptions<UploadOption> settings, ILogger<UploadFileService> logger)
    {
        _env = env;
        _logger = logger;
        _settings = settings.Value;
    }

    public async Task<IList<FileStorageUploadResponse>?> UploadFiles(StorageType uploadType,IList<FileStorageUploadRequest> files)
    {
        try
        {
            var response = new List<FileStorageUploadResponse>();
            foreach (var fileInfo in files)
            {
                if (fileInfo.File.Length == 0) throw new Exception("File is empty");
                var maxSize = _settings.MaximumFileSizeInMb * 1024 * 1024;
                if (fileInfo.File.Length > maxSize) throw new Exception("File size exceeded.");
                var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/Files/{uploadType.FolderName()}");
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                var file = new FileInfo(fileInfo.File.FileName);
                var fileName = $"{fileInfo.Id.ToString()}{file.Extension}";
                var fileNameWithPath = Path.Combine(path, fileName);
                await using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    await fileInfo.File.CopyToAsync(stream);
                }
                response.Add(new FileStorageUploadResponse(fileInfo.Id, fileNameWithPath));
            }
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError("Unhandled Exception. ID: {StackTrace} - Message: {Message}", ex.StackTrace, ex.Message);
            return null;
        }
    }
}
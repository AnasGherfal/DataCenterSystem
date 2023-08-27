using Infrastructure.Constants;
using Microsoft.Extensions.Options;
using Web.API.Options;
using Web.API.Services.UploadService.Dtos;

namespace Web.API.Services.UploadService;

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
                if (fileInfo.File.Length > _settings.MaximumFileSizeInMb) throw new Exception("File size exceeded.");
                var extension = Path.GetExtension(fileInfo.File.FileName);
                var fileName = $"{fileInfo.Id.ToString()}{extension}";
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), uploadType.FolderName(), fileName);
                await using var stream = new FileStream(filePath, FileMode.Create);
                await fileInfo.File.CopyToAsync(stream);
                response.Add(new FileStorageUploadResponse(fileInfo.Id, filePath));
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
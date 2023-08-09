using Infrastructure.Constants;

namespace Web.API.Services.UploadService;

public interface IUploadFileService
{
    Task<string> Upload(IFormFile file, StorageType type);
} 
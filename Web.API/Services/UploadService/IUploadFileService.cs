using Infrastructure.Constants;
using Web.API.Services.UploadService.Dtos;

namespace Web.API.Services.UploadService;

public interface IUploadFileService
{
    Task<IList<FileStorageUploadResponse>?> UploadFiles(StorageType type, IList<FileStorageUploadRequest> files);
} 
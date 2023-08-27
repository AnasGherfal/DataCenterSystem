using Core.Constants;
using Core.Interfaces.Dtos;

namespace Core.Interfaces.Services;

public interface IUploadFileService
{
    Task<IList<FileStorageUploadResponse>?> UploadFiles(StorageType type, IList<FileStorageUploadRequest> files);
} 
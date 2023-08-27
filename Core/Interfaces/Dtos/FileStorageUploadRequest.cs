using Microsoft.AspNetCore.Http;

namespace Core.Interfaces.Dtos;

public record FileStorageUploadRequest(Guid Id, IFormFile File, short FileType);
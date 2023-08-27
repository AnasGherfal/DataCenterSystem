namespace Web.API.Services.UploadService.Dtos;

public record FileStorageUploadRequest(Guid Id, IFormFile File, short FileType);
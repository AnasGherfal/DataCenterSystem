using ManagementAPI.Dtos;
using ManagementAPI.Dtos.Customer;
using ManagementAPI.Dtos.Invoice;
using Shared.Constants;
using Shared.Dtos;

namespace ManagementAPI.Services;

public interface IUploadFileService
{
    public Task<MessageResponse> Upload(FileRequestDto file,EntityType type, Object objct);
}

using ManagementAPI.Dtos.Service;
using Shared.Dtos;
namespace ManagementAPI.Services;

public interface IServiceServices
{
    public Task<MessageResponse> Create(CreateServiceDto request);
    public Task<FetchServicesResponseDto> GetAll(FetchServicesRequestDto request);
    public Task<MessageResponse> Update(Guid id, UpdateServiceDto request);
    public Task<MessageResponse> Delete(Guid id);
    public  Task<MessageResponse> Lock(Guid id);
    public  Task<MessageResponse> Unlock(Guid id);



   // public Task<OperationResponse> CreateService(CreateServiceDto request);
   // public Task<ICollection<ServiceResponseDto>> GetAllService();
   // public Task<ServiceResponseDto> GetService(int id);
}

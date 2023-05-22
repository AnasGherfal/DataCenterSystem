using ManagementAPI.Dtos.Customer;
using ManagementAPI.Dtos.Representive;
using Shared.Dtos;

namespace ManagementAPI.Services;

public interface IRepresentiveService
{
    public Task<MessageResponse> Create(CreateRepresentiveRequestDto request);
    public Task<RepresentiveResponseDto> GetById(int id, int customerId);
    public Task<FetchRepresentiveResponseDto> GetAll(FetchRepresentiveRequestDto request);
    public Task<MessageResponse> Lock(int id);
    public Task<MessageResponse> Unlock(int id);
    public Task<MessageResponse> Update(int id, UpdateRepresentiveRequestDto request);
    public Task<MessageResponse> Delete(int id);
}

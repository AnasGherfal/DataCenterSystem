using ManagementAPI.Dtos.Customer;
using ManagementAPI.Dtos.Representative;
using Shared.Dtos;

namespace ManagementAPI.Services;

public interface IRepresentativeService
{
    public Task<MessageResponse> Create(CreateRepresentativeRequestDto request);
    public Task<RepresentativeResponseDto> GetById(int id, int customerId);
    public Task<FetchRepresentativeResponseDto> GetAll(FetchRepresentativeRequestDto request);
    public Task<MessageResponse> Lock(int id);
    public Task<MessageResponse> Unlock(int id);
    public Task<MessageResponse> Update(int id, UpdateRepresentativeRequestDto request);
    public Task<MessageResponse> Delete(int id);
}

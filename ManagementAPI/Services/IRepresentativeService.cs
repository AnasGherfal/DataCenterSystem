using ManagementAPI.Dtos.Customer;
using ManagementAPI.Dtos.Representative;
using Shared.Dtos;

namespace ManagementAPI.Services;

public interface IRepresentativeService
{
    public Task<MessageResponse> Create(CreateRepresentativeRequestDto request);
    public Task<RepresentativeResponseDto> GetById(Guid id);
    public Task<FetchRepresentativeResponseDto> GetAll(FetchRepresentativeRequestDto request);
    public Task<MessageResponse> Lock(Guid id);
    public Task<MessageResponse> Unlock(Guid id);
    public Task<MessageResponse> Update(Guid id, UpdateRepresentativeRequestDto request);
    public Task<MessageResponse> Delete(Guid id);
}

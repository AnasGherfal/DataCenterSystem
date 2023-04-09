using ManagementAPI.Dtos.Customer;
using ManagementAPI.Dtos.Representive;
using Shared.Dtos;

namespace ManagementAPI.Services;

public interface IRepresentiveService
{
    public Task<OperationResponse> Create(CreateRepresentiveRequestDto request);
    public Task<FetchRepresentivesResponseDto> GetAll(FetchRepresentivesRequestDto request);
    public Task<OperationResponse> Lock(int id);
    public Task<OperationResponse> Unlock(int id);
    public Task<OperationResponse> Update(int id, UpdateRepresentiveRequestDto request);
    public Task<OperationResponse> Delete(int id);
}

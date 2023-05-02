using ManagementAPI.Dtos.Companion;
using Shared.Dtos;

namespace ManagementAPI.Services;

public interface ICompanionService
{
    public Task<OperationResponse> Create(CreateCompanionRequestDto request);
    public Task<FetchCompanionResponseDto> GetAll(FetchCompanionRequestDto request);
    public Task<OperationResponse> Update(int id, UpdateCompanionRequestDto request);
    public Task<OperationResponse> Delete(int id);
}

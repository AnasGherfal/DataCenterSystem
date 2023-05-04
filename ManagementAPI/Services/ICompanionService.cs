using ManagementAPI.Dtos.Companion;
using Shared.Dtos;

namespace ManagementAPI.Services;

public interface ICompanionService
{
    public Task<MessageResponse> Create(CreateCompanionRequestDto request);
    public Task<FetchCompanionResponseDto> GetAll(FetchCompanionRequestDto request);
    public Task<MessageResponse> Update(int id, UpdateCompanionRequestDto request);
    public Task<MessageResponse> Delete(int id);
}

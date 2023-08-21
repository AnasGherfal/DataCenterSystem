using ManagementAPI.Dtos.VisitTimeShift;
using Shared.Dtos;

namespace ManagementAPI.Services;

public interface IVisitTimeShiftService
{
    public Task<MessageResponse> Create(CreateVisitTimeShiftRequestDto request);
    public Task<FetchVisitTimeShiftResponseDto> GetAll(FetchVisitTimeShiftRequestDto request);
    public Task<MessageResponse> Lock(Guid id);
    public Task<MessageResponse> Unlock(Guid id);
    public Task<MessageResponse> Update(Guid id, UpdateVisitTimeShiftRequestDto request);
    public Task<MessageResponse> Delete(Guid id);
}

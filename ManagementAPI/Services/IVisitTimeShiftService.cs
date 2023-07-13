using ManagementAPI.Dtos.VisitTimeShift;
using Shared.Dtos;

namespace ManagementAPI.Services;

public interface IVisitTimeShiftService
{
    public Task<OperationResponse> Create(CreateVisitTimeShiftRequestDto request);
    public Task<FetchVisitTimeShiftResponseDto> GetAll(FetchVisitTimeShiftRequestDto request);
    public Task<OperationResponse> Lock(Guid id);
    public Task<OperationResponse> Unlock(Guid id);
    public Task<OperationResponse> Update(Guid id, UpdateVisitTimeShiftRequestDto request);
    public Task<OperationResponse> Delete(Guid id);
}

using ManagementAPI.Dtos.VisitTimeShift;
using Shared.Dtos;

namespace ManagementAPI.Services;

public interface IVisitTimeShiftService
{
    public Task<OperationResponse> Create(CreateVisitTimeShiftRequestDto request);
    public Task<FetchVisitTimeShiftResponseDto> GetAll(FetchVisitTimeShiftRequestDto request);
    public Task<OperationResponse> Lock(int id);
    public Task<OperationResponse> Unlock(int id);
    public Task<OperationResponse> Update(int id, UpdateVisitTimeShiftRequestDto request);
    public Task<OperationResponse> Delete(int id);
}

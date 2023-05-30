using ManagementAPI.Dtos.Companion;
using ManagementAPI.Dtos.Visit;
using Shared.Dtos;

namespace ManagementAPI.Services;

public interface IVisitService
{
    public Task<MessageResponse> Create(CreateVisitRequestDto request);
    public Task<FetchVisitResponseDto> GetAll(FetchVisitRequestDto request);
    public Task<MessageResponse> Lock(int id);
    public Task<MessageResponse> Unlock(int id);
    public Task<MessageResponse> Update(int id, UpdateVisitRequestDto request);
    //public Task<OperationResponse> UpdateCompanion(int visitId,int companionId, UpdateCompanionRequestDto request);
    public Task<MessageResponse> Delete(int id);
    public Task<MessageResponse> Paid(int id, int InvoiceId);
}

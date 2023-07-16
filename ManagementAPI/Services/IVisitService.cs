using ManagementAPI.Dtos.Companion;
using ManagementAPI.Dtos.Visit;
using Shared.Dtos;

namespace ManagementAPI.Services;

public interface IVisitService
{
    public Task<MessageResponse> Create(CreateVisitRequestDto request);
    public Task<FetchVisitResponseDto> GetAll(FetchVisitRequestDto request);
    public Task<VisitResponseDto> GetById(Guid id);
    public Task<MessageResponse> Lock(Guid id);
    public Task<MessageResponse> Unlock(Guid id);
    public Task<MessageResponse> Update(Guid id, UpdateVisitRequestDto request);
    //public Task<OperationResponse> UpdateCompanion(int visitId,int companionId, UpdateCompanionRequestDto request);
    public Task<MessageResponse> Delete(Guid id);
    public Task<MessageResponse> Paid(Guid id, Guid InvoiceId);
}

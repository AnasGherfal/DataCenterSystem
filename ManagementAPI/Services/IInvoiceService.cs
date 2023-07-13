using ManagementAPI.Dtos.Invoice;
using Shared.Dtos;

namespace ManagementAPI.Services;

public interface IInvoiceService
{
    public Task<MessageResponse> Create(CreateInvoiceRequestDto request);
    public Task<FetchInvoicesResponseDto> GetAll(FetchInvoicesRequestDto request);
    public Task<InvoiceResponseDto> GetById (Guid id);
    public Task<MessageResponse> Update(Guid id, UpdateInvoiceRequestDto request);
    public Task<MessageResponse> Delete(Guid id);
    public Task<MessageResponse> Lock(Guid id);
    public Task<MessageResponse> Unlock(Guid id);
}

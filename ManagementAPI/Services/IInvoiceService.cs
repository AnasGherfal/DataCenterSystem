using ManagementAPI.Dtos.Invoice;
using Shared.Dtos;

namespace ManagementAPI.Services;

public interface IInvoiceService
{
    public Task<MessageResponse> Create(CreateInvoiceRequestDto request);
    public Task<FetchInvoicesResponseDto> GetAll(FetchInvoicesRequestDto request);
    public Task<InvoiceResponseDto> GetById (int id);
    public Task<MessageResponse> Update(int id, UpdateInvoiceRequestDto request);
    public Task<MessageResponse> Delete(int id);
    public Task<MessageResponse> Lock(int id);
    public Task<MessageResponse> Unlock(int id);
}

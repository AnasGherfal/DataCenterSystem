using ManagementAPI.Dtos;
using ManagementAPI.Dtos.Customer;
using ManagementAPI.Dtos.Subscriptions;
using Shared.Dtos;

namespace ManagementAPI.Services;

public interface ISubscriptionService
{
    public Task<MessageResponse> Create(CreateSubscriptionRequestDto request);
    public Task<SubscriptionRsponseDto> GetById(Guid id);
    public Task<FetchSubscriptionResponseDto> GetAll(FetchSubscriptionRequestDto request);
    public Task<FetchSubscriptionFileResponseDto> GetFiles(FetchSubscriptionRequestDto request);
    public Task<List<SubscriptionFileResponsDto>> GetFileById(Guid id);
    public  Task<MessageResponse> UpdateFile(Guid id, FileRequestDto request);
    public Task<MessageResponse> DeleteFile(Guid id);
    public Task<FileStream> Download(Guid id);
    public  Task<MessageResponse> Renew(Guid id);
    public Task<MessageResponse> Update(Guid id, UpdateSubscriptionRequestDto request);
    public Task<MessageResponse> Lock(Guid id);
    public Task<MessageResponse> Unlock(Guid id);
    public Task<MessageResponse> Delete(Guid id);
}

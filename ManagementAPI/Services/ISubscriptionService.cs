using ManagementAPI.Dtos;
using ManagementAPI.Dtos.Customer;
using ManagementAPI.Dtos.Subscriptions;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;

namespace ManagementAPI.Services;

public interface ISubscriptionService
{
    public Task<MessageResponse> Create(CreateSubscriptionRequestDto request);
    public Task<SubscriptionRsponseDto> GetById(Guid id);
    public Task<FetchSubscriptionResponseDto> GetAll(FetchSubscriptionRequestDto request);
    public  Task<MessageResponse> Renew( FileRequestDto file,Guid id);
    public Task<MessageResponse> Update(Guid id, UpdateSubscriptionRequestDto request);
    public Task<MessageResponse> Lock(Guid id);
    public Task<MessageResponse> Unlock(Guid id);
    public Task<MessageResponse> Delete(Guid id);
    public Task<FileStreamResult> Download(Guid id);
}

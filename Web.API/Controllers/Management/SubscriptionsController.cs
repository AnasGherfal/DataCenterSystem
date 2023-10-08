using Core.Constants;
using Core.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Web.API.Abstracts;
using Web.API.Features.SubscriptionManagement.CreateSubscription;
using Web.API.Features.SubscriptionManagement.DeleteSubscription;
using Web.API.Features.SubscriptionManagement.FetchSubscriptionById;
using Web.API.Features.SubscriptionManagement.FetchSubscriptionFileById;
using Web.API.Features.SubscriptionManagement.FetchSubscriptions;
using Web.API.Features.SubscriptionManagement.LockSubscription;
using Web.API.Features.SubscriptionManagement.RenewSubscription;
using Web.API.Features.SubscriptionManagement.UnlockSubscription;
using Web.API.Filters;

namespace Web.API.Controllers.Management;

// [VerifiedAdmin(SystemPermissions.SubscriptionManagement)]
[ApiController]
public class SubscriptionsController : ManagementController
{
    [HttpPost]
    public async Task<MessageResponse> Create([FromForm] CreateSubscriptionCommand request) 
        => await Mediator.Send(request);
    
    [HttpGet]
    public async Task<PagedResponse<FetchSubscriptionsQueryResponse>> Fetch([FromQuery] FetchSubscriptionsQuery request) 
        => await Mediator.Send(request);
    
    [HttpGet("{id}")]
    public async Task<ContentResponse<FetchSubscriptionByIdQueryResponse>> FetchById(string id)
        => await Mediator.Send(new FetchSubscriptionByIdQuery()
        {
            Id = id,
        });
    
    [HttpGet("{id}/document/{fileId}")]
    public async Task<FileStreamResult> FetchById(string id, string fileId)
        => await Mediator.Send(new FetchSubscriptionFileByIdQuery()
        {
            SubscriptionId = id,
            FileId = fileId,
        });

    
    [HttpPut("{id}/renew")]
    public async Task<MessageResponse> Renew(string id, [FromBody] RenewSubscriptionCommand request)
        => await Mediator.Send(new RenewSubscriptionCommand()
        {
            Id = id,
        });

    [HttpPut("{id}/lock")]
    public async Task<MessageResponse> Lock(string id) 
        => await Mediator.Send(new LockSubscriptionCommand()
        {
            Id = id,
        });
    
    [HttpPut("{id}/unlock")]
    public async Task<MessageResponse> UnLock(string id) 
        => await Mediator.Send(new UnlockSubscriptionCommand()
        {
            Id = id,
        });
    
    [HttpDelete("{id}")]
    public async Task<MessageResponse> Delete(string id) 
        => await Mediator.Send(new DeleteSubscriptionCommand()
        {
            Id = id,
        });
}
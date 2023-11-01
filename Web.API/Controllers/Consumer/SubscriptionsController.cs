using Core.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Web.API.Abstracts;
using Web.API.Features.Consumer.SubscriptionsManagement.FetchMySubscriptionById;
using Web.API.Features.Consumer.SubscriptionsManagement.FetchMySubscriptionFileById;
using Web.API.Features.Consumer.SubscriptionsManagement.FetchMySubscriptions;
using Web.API.Features.Consumer.SubscriptionsManagement.RequestNewSubscription;
using Web.API.Filters;

namespace Web.API.Controllers.Consumer;

[VerifiedCustomer]
[ApiController]
public class SubscriptionsController : ConsumerController
{
    [HttpPost]
    public async Task<MessageResponse> Create([FromForm] RequestNewSubscriptionCommand request) 
        => await Mediator.Send(request);
    
    [HttpGet]
    public async Task<PagedResponse<FetchMySubscriptionsQueryResponse>> Fetch([FromQuery] FetchMySubscriptionsQuery request) 
        => await Mediator.Send(request);
    
    [HttpGet("{id}")]
    public async Task<ContentResponse<FetchMySubscriptionByIdQueryResponse>> FetchById(string id)
        => await Mediator.Send(new FetchMySubscriptionByIdQuery()
        {
            Id = id,
        });
    
    [HttpGet("{id}/document/{fileId}")]
    public async Task<FileStreamResult> FetchById(string id, string fileId)
        => await Mediator.Send(new FetchMySubscriptionFileByIdQuery()
        {
            SubscriptionId = id,
            FileId = fileId,
        });
}
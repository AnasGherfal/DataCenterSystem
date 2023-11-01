using Core.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Web.API.Abstracts;
using Web.API.Features.Consumer.VisitsManagement.CancelVisit;
using Web.API.Features.Consumer.VisitsManagement.FetchMyVisitById;
using Web.API.Features.Consumer.VisitsManagement.FetchMyVisits;
using Web.API.Features.Consumer.VisitsManagement.RequestNewVisit;
using Web.API.Features.VisitsManagement.CreateVisit;
using Web.API.Features.VisitsManagement.DeleteVisit;
using Web.API.Features.VisitsManagement.FetchVisitById;
using Web.API.Features.VisitsManagement.FetchVisits;
using Web.API.Filters;

namespace Web.API.Controllers.Consumer;

[VerifiedCustomer]
[ApiController]
public class VisitsController : ConsumerController
{
    [HttpPost]
    public async Task<MessageResponse> Create([FromBody] RequestNewVisitCommand request) 
        => await Mediator.Send(request);
    
    [HttpGet]
    public async Task<PagedResponse<FetchMyVisitsQueryResponse>> Fetch([FromQuery] FetchMyVisitsQuery request) 
        => await Mediator.Send(request);
    
    [HttpGet("{id}")]
    public async Task<ContentResponse<FetchMyVisitByIdQueryResponse>> FetchById(string id)
        => await Mediator.Send(new FetchMyVisitByIdQuery()
        {
            Id = id,
        });

    [HttpDelete("{id}")]
    public async Task<MessageResponse> Delete(string id)
        => await Mediator.Send(new CancelVisitCommand()
        {
            Id = id,
        });
}
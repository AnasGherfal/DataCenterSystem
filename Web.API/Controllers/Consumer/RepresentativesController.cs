using Core.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Web.API.Abstracts;
using Web.API.Features.Consumer.RepresentativesManagement.FetchMyRepresentativeById;
using Web.API.Features.Consumer.RepresentativesManagement.FetchMyRepresentativeFileById;
using Web.API.Features.Consumer.RepresentativesManagement.FetchMyRepresentatives;
using Web.API.Features.Consumer.RepresentativesManagement.RequestNewRepresentative;
using Web.API.Filters;

namespace Web.API.Controllers.Consumer;

[VerifiedCustomer]
[ApiController]
public class RepresentativesController : ConsumerController
{
    [HttpPost]
    public async Task<MessageResponse> Create([FromForm] RequestNewRepresentativeCommand request) 
        => await Mediator.Send(request);
    
    [HttpGet]
    public async Task<PagedResponse<FetchMyRepresentativesQueryResponse>> Fetch([FromQuery] FetchMyRepresentativesQuery request) 
        => await Mediator.Send(request);
    
    [HttpGet("{id}")]
    public async Task<ContentResponse<FetchMyRepresentativeByIdQueryResponse>> FetchById(string id)
        => await Mediator.Send(new FetchMyRepresentativeByIdQuery()
        {
            Id = id,
        });
    
    [HttpGet("{id}/document/{fileId}")]
    public async Task<FileStreamResult> FetchById(string id, string fileId)
        => await Mediator.Send(new FetchMyRepresentativeFileByIdQuery()
        {
            RepresentativeId = id,
            FileId = fileId,
        });
}
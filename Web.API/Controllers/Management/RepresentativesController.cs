using Core.Constants;
using Core.Dtos;
using Microsoft.AspNetCore.Mvc;
using Web.API.Abstracts;
using Web.API.Features.RepresentativeManagement.CreateRepresentative;
using Web.API.Features.RepresentativeManagement.DeleteRepresentative;
using Web.API.Features.RepresentativeManagement.FetchRepresentativeById;
using Web.API.Features.RepresentativeManagement.FetchRepresentativeFileById;
using Web.API.Features.RepresentativeManagement.FetchRepresentatives;
using Web.API.Features.RepresentativeManagement.LockRepresentative;
using Web.API.Features.RepresentativeManagement.UnlockRepresentative;
using Web.API.Features.RepresentativeManagement.UpdateRepresentative;
using Web.API.Filters;

namespace Web.API.Controllers.Management;

[VerifiedAdmin(SystemPermissions.RepresentativeManagement)]
[ApiController]
public class RepresentativesController : ManagementController
{
    [HttpPost]
    public async Task<MessageResponse> Create([FromForm] CreateRepresentativeCommand request) 
        => await Mediator.Send(request);
    
    [HttpGet]
    public async Task<PagedResponse<FetchRepresentativesQueryResponse>> Fetch([FromQuery] FetchRepresentativesQuery request) 
        => await Mediator.Send(request);
    
    [HttpGet("{id}")]
    public async Task<ContentResponse<FetchRepresentativeByIdQueryResponse>> FetchById(string id)
        => await Mediator.Send(new FetchRepresentativeByIdQuery()
        {
            Id = id,
        });
    
    [HttpGet("{id}/document/{fileId}")]
    public async Task<FileStreamResult> FetchById(string id, string fileId)
        => await Mediator.Send(new FetchRepresentativeFileByIdQuery()
        {
            RepresentativeId = id,
            FileId = fileId,
        });


    [HttpPut("{id}")]
    public async Task<MessageResponse> Renew(string id, [FromBody] UpdateRepresentativeCommand request)
    {
        request.SetId(id);
        return await Mediator.Send(request);
    }

    [HttpPut("{id}/lock")]
    public async Task<MessageResponse> Lock(string id) 
        => await Mediator.Send(new LockRepresentativeCommand()
        {
            Id = id,
        });
    
    [HttpPut("{id}/unlock")]
    public async Task<MessageResponse> UnLock(string id) 
        => await Mediator.Send(new UnlockRepresentativeCommand()
        {
            Id = id,
        });
    
    [HttpDelete("{id}")]
    public async Task<MessageResponse> Delete(string id) 
        => await Mediator.Send(new DeleteRepresentativeCommand()
        {
            Id = id,
        });
}
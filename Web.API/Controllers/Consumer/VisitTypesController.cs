using Core.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Web.API.Abstracts;
using Web.API.Features.VisitTypesManagement.FetchVisitTypes;
using Web.API.Filters;

namespace Web.API.Controllers.Consumer;

[VerifiedCustomer]
[ApiController]
public class VisitTypesController : ConsumerController
{
    [HttpGet]
    public async Task<ListResponse<FetchVisitTypesQueryResponse>> Fetch() 
        => await Mediator.Send(new FetchVisitTypesQuery());
}
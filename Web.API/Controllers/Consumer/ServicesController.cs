using Core.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Web.API.Abstracts;
using Web.API.Features.Consumer.ServiceManagement.FetchServices;
using Web.API.Filters;

namespace Web.API.Controllers.Consumer;

[VerifiedCustomer]
[ApiController]
public class ServicesController : ConsumerController
{
    [HttpGet]
    public async Task<PagedResponse<FetchServicesQueryResponse>> Fetch([FromQuery] FetchServicesQuery request) 
        => await Mediator.Send(request);
}
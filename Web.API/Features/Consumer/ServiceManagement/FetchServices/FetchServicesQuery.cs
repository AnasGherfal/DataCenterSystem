using Core.Wrappers;
using MediatR;

namespace Web.API.Features.Consumer.ServiceManagement.FetchServices;
public sealed record FetchServicesQuery: IRequest<PagedResponse<FetchServicesQueryResponse>>
{
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}

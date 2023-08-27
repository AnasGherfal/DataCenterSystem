using MediatR;
using Shared.Dtos;

namespace Web.API.Features.ServiceManagement.FetchServices;
public sealed record FetchServicesQuery: IRequest<PagedResponse<FetchServicesQueryResponse>>
{
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}

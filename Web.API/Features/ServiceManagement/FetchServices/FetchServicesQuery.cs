using MediatR;
using Shared.Dtos;

namespace Web.API.Features.ServiceManagement.FetchServices;
public sealed record FetchServicesQuery(int? PageNumber, int? PageSize) 
    : IRequest<PagedResponse<FetchServicesQueryResponse>>;

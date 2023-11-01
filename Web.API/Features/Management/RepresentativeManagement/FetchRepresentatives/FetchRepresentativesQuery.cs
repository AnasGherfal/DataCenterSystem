using Core.Wrappers;
using MediatR;
using Web.API.Features.RepresentativeManagement.FetchRepresentatives;

namespace Web.API.Features.Management.RepresentativeManagement.FetchRepresentatives;
public sealed record FetchRepresentativesQuery : IRequest<PagedResponse<FetchRepresentativesQueryResponse>>
{
    public string? CustomerId { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}
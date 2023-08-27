using Core.Dtos;
using MediatR;

namespace Web.API.Features.RepresentativeManagement.FetchRepresentatives;
public sealed record FetchRepresentativesQuery : IRequest<PagedResponse<FetchRepresentativesQueryResponse>>
{
    public string? CustomerId { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}
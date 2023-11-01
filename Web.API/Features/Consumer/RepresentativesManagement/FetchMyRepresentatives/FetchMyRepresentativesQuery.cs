using Core.Wrappers;
using MediatR;

namespace Web.API.Features.Consumer.RepresentativesManagement.FetchMyRepresentatives;
public sealed record FetchMyRepresentativesQuery : IRequest<PagedResponse<FetchMyRepresentativesQueryResponse>>
{
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}
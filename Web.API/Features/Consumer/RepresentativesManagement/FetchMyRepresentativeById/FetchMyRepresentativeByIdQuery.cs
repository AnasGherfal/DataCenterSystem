using Core.Wrappers;
using MediatR;

namespace Web.API.Features.Consumer.RepresentativesManagement.FetchMyRepresentativeById;

public sealed record FetchMyRepresentativeByIdQuery: IRequest<ContentResponse<FetchMyRepresentativeByIdQueryResponse>>
{
    public string? Id { get; set; }
}

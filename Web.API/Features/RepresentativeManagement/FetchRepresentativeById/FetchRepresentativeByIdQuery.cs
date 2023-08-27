using MediatR;
using Shared.Dtos;

namespace Web.API.Features.RepresentativeManagement.FetchRepresentativeById;

public sealed record FetchRepresentativeByIdQuery: IRequest<ContentResponse<FetchRepresentativeByIdQueryResponse>>
{
    public string? Id { get; set; }
}

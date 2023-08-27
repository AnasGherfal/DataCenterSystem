using MediatR;
using Shared.Dtos;

namespace Web.API.Features.VisitsManagement.FetchVisitById;

public sealed record FetchVisitByIdQuery: IRequest<ContentResponse<FetchVisitByIdQueryResponse>>
{
    public string? Id { get; set; }
}

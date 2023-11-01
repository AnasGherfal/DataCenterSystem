using Core.Wrappers;
using MediatR;

namespace Web.API.Features.Consumer.VisitsManagement.FetchMyVisitById;

public sealed record FetchMyVisitByIdQuery: IRequest<ContentResponse<FetchMyVisitByIdQueryResponse>>
{
    public string? Id { get; set; }
}

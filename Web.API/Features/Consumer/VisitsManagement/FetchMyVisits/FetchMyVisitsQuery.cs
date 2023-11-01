using Core.Wrappers;
using MediatR;

namespace Web.API.Features.Consumer.VisitsManagement.FetchMyVisits;
public sealed record FetchMyVisitsQuery : IRequest<PagedResponse<FetchMyVisitsQueryResponse>>
{
    public string? SubscriptionId { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}
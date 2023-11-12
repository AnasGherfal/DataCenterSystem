using Core.Wrappers;
using MediatR;

namespace Web.API.Features.VisitsManagement.FetchVisits;
public sealed record FetchVisitsQuery : IRequest<PagedResponse<FetchVisitsQueryResponse>>
{
    public string? CustomerId { get; set; }
    public string? SubscriptionId { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}
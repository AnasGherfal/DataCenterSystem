using Core.Wrappers;
using MediatR;

namespace Web.API.Features.Management.AuditManagement.FetchAudits;
public sealed record FetchAuditsQuery : IRequest<PagedResponse<FetchAuditsQueryResponse>>
{
    public string? RecordId { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}
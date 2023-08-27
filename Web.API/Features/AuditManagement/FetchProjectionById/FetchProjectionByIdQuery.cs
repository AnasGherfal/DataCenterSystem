using Core.Constants;
using Core.Wrappers;
using MediatR;

namespace Web.API.Features.AuditManagement.FetchProjectionById;
public sealed record FetchProjectionByIdQuery: IRequest<ContentResponse<FetchProjectionByIdQueryResponse>>
{
    public EntityType? ProjectionType { get; set; }
    public string? Id { get; private set; }
    public void SetId(string id) => Id = id;
}

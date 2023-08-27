using MediatR;
using Shared.Dtos;

namespace Web.API.Features.TimeShiftManagement.FetchTimeShiftById;
public sealed record FetchTimeShiftByIdQuery: IRequest<ContentResponse<FetchTimeShiftByIdQueryResponse>>
{
    public string? Id { get; set; }
}

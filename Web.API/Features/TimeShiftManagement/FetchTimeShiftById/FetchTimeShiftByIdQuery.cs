using Core.Dtos;
using MediatR;

namespace Web.API.Features.TimeShiftManagement.FetchTimeShiftById;
public sealed record FetchTimeShiftByIdQuery: IRequest<ContentResponse<FetchTimeShiftByIdQueryResponse>>
{
    public string? Id { get; set; }
}

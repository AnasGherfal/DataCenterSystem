using MediatR;
using Shared.Dtos;

namespace Web.API.Features.TimeShiftManagement.FetchTimeShifts;
public sealed record FetchTimeShiftsQuery: IRequest<PagedResponse<FetchTimeShiftsQueryResponse>>
{
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}

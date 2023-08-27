using Core.Wrappers;
using Infrastructure;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Features.TimeShiftManagement.FetchTimeShifts;

public sealed record FetchTimeShiftsQueryHandler : IRequestHandler<FetchTimeShiftsQuery, PagedResponse<FetchTimeShiftsQueryResponse>>
{
    private readonly AppDbContext _dbContext;

    public FetchTimeShiftsQueryHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResponse<FetchTimeShiftsQueryResponse>> Handle(FetchTimeShiftsQuery request, CancellationToken cancellationToken)
    {
        var pageNumber = request.PageNumber ?? 1;
        var pageSize = request.PageSize ?? 5;
        var data = await _dbContext.TimeShifts
            .OrderBy(p => p.CreatedOn)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .Select(p => new FetchTimeShiftsQueryResponse()
            {
                Id = p.Id,
                Date = p.Date,
                Day = p.Day,
                StartTime = p.StartTime,
                EndTime = p.EndTime,
                PriceForFirstHour = p.PriceForFirstHour,
                PriceForRemainingHours = p.PriceForRemainingHours,
                CreatedOn = p.CreatedOn,
            })
            .ToListAsync(cancellationToken: cancellationToken);
        var count = await _dbContext.TimeShifts.CountAsync(cancellationToken: cancellationToken);
        return new PagedResponse<FetchTimeShiftsQueryResponse>("", data, count, pageNumber, pageSize);
    }
}
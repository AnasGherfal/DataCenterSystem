using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Exceptions;

namespace Web.API.Features.TimeShiftManagement.FetchTimeShiftById;

public sealed record FetchTimeShiftByIdQueryHandler : IRequestHandler<FetchTimeShiftByIdQuery, ContentResponse<FetchTimeShiftByIdQueryResponse>>
{
    private readonly AppDbContext _dbContext;

    public FetchTimeShiftByIdQueryHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ContentResponse<FetchTimeShiftByIdQueryResponse>> Handle(FetchTimeShiftByIdQuery request, CancellationToken cancellationToken)
    {
        var data = await _dbContext.TimeShifts
            .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id!), cancellationToken: cancellationToken);
        if (data == null) throw new NotFoundException("TIME_SHIFT_NOT_FOUND");
        return new ContentResponse<FetchTimeShiftByIdQueryResponse>("", new FetchTimeShiftByIdQueryResponse()
        {
            Id = data.Id,
            Date = data.Date,
            Day = data.Day,
            StartTime = data.StartTime,
            EndTime = data.EndTime,
            PriceForFirstHour = data.PriceForFirstHour,
            PriceForRemainingHours = data.PriceForRemainingHours,
            CreatedOn = data.CreatedOn,
        });
    }
}
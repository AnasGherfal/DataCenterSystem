using Infrastructure;
using Infrastructure.Constants;
using Infrastructure.Entities;
using Infrastructure.Events.Visit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Exceptions;
using Web.API.Services.ClientService;

namespace Web.API.Features.VisitsManagement.EndVisit;

public sealed record EndVisitCommandHandler : IRequestHandler<EndVisitCommand, MessageResponse>
{
    private readonly IClientService _client;
    private readonly AppDbContext _dbContext;

    public EndVisitCommandHandler(AppDbContext dbContext, IClientService client)
    {
        _dbContext = dbContext;
        _client = client;
    }

    public async Task<MessageResponse> Handle(EndVisitCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.Parse(request.Id!);
        var data = await _dbContext.Visits
                       .SingleOrDefaultAsync(p => p.Id == id, cancellationToken: cancellationToken);
        if (data == null) throw new NotFoundException("VISIT_NOT_FOUND");
        if (data.Status == GeneralStatus.Locked) throw new BadRequestException("VISIT_LOCKED");
        if (data.StartTime == null) throw new BadRequestException("VISIT_HAS_NOT_STARTED");
        if (data.EndTime != null) throw new BadRequestException("VISIT_ALREADY_ENDED");
        var endDate = request.EndTime ?? DateTime.UtcNow;
        if (endDate < data.StartTime) throw new BadRequestException("VISIT_END_TIME_IS_BEFORE_START_TIME");
        if ((endDate.Date.DayOfWeek != data.StartTime.Value.Date.DayOfWeek) || (endDate.Date.AddDays(-1).DayOfWeek != data.StartTime.Value.Date.DayOfWeek)) 
            throw new BadRequestException("VISIT_DAY_DIFFERENCE_MORE_THAN_ONE_DAY");
        var totalPrice = 0M;
        // if (data.StartTime.Value.Date == DateTime.UtcNow.Date)
        // {
            // Visit is in the same day
            //TODO: Calculate Price For Special Days
            var startTimeSpan = data.StartTime!.Value.TimeOfDay;
            var endTimeSpan = endDate.TimeOfDay;
            var timeShifts = await _dbContext.TimeShifts
                .Where(ts => ts.Date == null
                    && ts.Day == data.StartTime.Value.DayOfWeek || ts.Day == endDate.DayOfWeek
                    && ts.StartTime <= startTimeSpan 
                    && ts.EndTime >= endTimeSpan)
                .ToListAsync(cancellationToken: cancellationToken);
            if (timeShifts.Count == 0) throw new BadRequestException("VISIT_TIME_SHIFT_NOT_FOUND");
            totalPrice = CalculatePrice(timeShifts, data.StartTime.Value, endDate);
        // }
        var @event = new VisitEndedEvent(_client.GetIdentifier(), data.Id, data.Sequence + 1, new VisitEndedEventData()
        {
            EndTime = endDate,
            TotalTime = endDate - data.StartTime!.Value,
            Price = totalPrice,
        });
        data.Apply(@event);
        _dbContext.Entry(data).State = EntityState.Modified;
        await _dbContext.Events.AddAsync(@event, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new MessageResponse()
        {
            Msg = "VISIT_ENDED",
        };
    }

    private decimal CalculatePrice(IList<TimeShift> timeShifts, DateTime start, DateTime end)
    {
        var totalPrice = 0M;
        var startTimeSpan = start.TimeOfDay;
        var endTimeSpan = end.TimeOfDay;
        foreach (var timeShift in timeShifts)
        {
            var overlapStart = startTimeSpan < timeShift.EndTime ? timeShift.StartTime : startTimeSpan;
            var overlapEnd = endTimeSpan > timeShift.StartTime ? timeShift.EndTime : endTimeSpan;
            var overlapDuration = overlapEnd - overlapStart;
            if (overlapDuration <= TimeSpan.Zero) continue;
            var firstHourPrice = timeShift.PriceForFirstHour;
            var remainingHoursPrice = timeShift.PriceForRemainingHours * (decimal)(overlapDuration.TotalHours - 1);
            var timeShiftPrice = firstHourPrice + remainingHoursPrice;
            totalPrice += timeShiftPrice;
        }
        return totalPrice;
    }
}
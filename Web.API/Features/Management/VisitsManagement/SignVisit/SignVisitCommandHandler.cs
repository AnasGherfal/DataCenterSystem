using Core.Constants;
using Core.Entities;
using Core.Events.Visit;
using Core.Exceptions;
using Core.Interfaces.Services;
using Core.Wrappers;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Features.Management.VisitsManagement.SignVisit;

public sealed record SignVisitCommandHandler : IRequestHandler<SignVisitCommand, MessageResponse>
{
    private readonly IClientService _client;
    private readonly AppDbContext _dbContext;

    public SignVisitCommandHandler(AppDbContext dbContext, IClientService client)
    {
        _dbContext = dbContext;
        _client = client;
    }

    public async Task<MessageResponse> Handle(SignVisitCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.Parse(request.Id!);
        var data = await _dbContext.Visits
                       .SingleOrDefaultAsync(p => p.Id == id, cancellationToken: cancellationToken);
        if (data == null) throw new NotFoundException("VISIT_NOT_FOUND");
        if (data.Status == GeneralStatus.Locked) throw new BadRequestException("VISIT_LOCKED");
        if (data.StartTime != null && data.EndTime != null) throw new BadRequestException("VISIT_ALREADY_SIGNED");
        var customerIsActive = await _dbContext.Customers
            .AnyAsync(c => c.Id == data.CustomerId && c.Status == GeneralStatus.Active, cancellationToken: cancellationToken);
        if (!customerIsActive) throw new BadRequestException("CUSTOMER_NOT_ACTIVE");
        var subscriptionIsActive = await _dbContext.Subscriptions
            .AnyAsync(s => s.Id == data.SubscriptionId && s.Status == GeneralStatus.Active, cancellationToken: cancellationToken);
        if (!subscriptionIsActive) throw new BadRequestException("SUBSCRIPTION_NOT_ACTIVE");
        
        var startTime = DateTime.Parse(request.StartTime ?? "");// ?? DateTime.UtcNow);
        var endTime = DateTime.Parse(request.EndTime ?? "");// ?? DateTime.UtcNow);
        if (endTime <= startTime) throw new BadRequestException("VISIT_END_TIME_IS_BEFORE_START_TIME");
        if (endTime.Date != startTime.Date)
        {
            var afterEndDayNotStartDay = endTime.Date.AddDays(-1).DayOfWeek != startTime.Date.DayOfWeek;
            if (afterEndDayNotStartDay) throw new BadRequestException("VISIT_DAY_DIFFERENCE_MORE_THAN_ONE_DAY");
        }
        var totalPrice = 0M;
        //TODO: Calculate Price For Special Days
        var startTimeSpan = startTime.TimeOfDay;
        var endTimeSpan = endTime.TimeOfDay;
        var timeShifts = await _dbContext.TimeShifts
            .Where(ts => ts.Date == null
                && ts.Day == startTime.DayOfWeek || ts.Day == endTime.DayOfWeek
                && ts.StartTime <= startTimeSpan 
                && ts.EndTime >= endTimeSpan)
            .ToListAsync(cancellationToken: cancellationToken);
        if (timeShifts.Count == 0) throw new BadRequestException("VISIT_TIME_SHIFT_NOT_FOUND");
        totalPrice = CalculatePrice(timeShifts, startTime, endTime);
        var @event = new VisitSignedEvent(_client.GetIdentifier(), data.Id, data.Sequence + 1, new VisitSignedEventData()
        {
            StartTime = startTime,
            EndTime = endTime,
            TotalTime = endTime - startTime,
            Price = totalPrice,
        });
        data.Apply(@event);
        _dbContext.Entry(data).State = EntityState.Modified;
        await _dbContext.Events.AddAsync(@event, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new MessageResponse()
        {
            Msg = "VISIT_SIGNED",
        };
    }
    
    private decimal CalculatePrice(IList<TimeShift> timeShifts, DateTime start, DateTime end)
    {
        var totalPrice = 0M;
        var startTimeSpan = start.TimeOfDay;
        var endTimeSpan = end.TimeOfDay;
        //[Edited!] checking visit duration if its Greater than one hour Deal with it
        //& if its less than or equal to one hour even with overlap just take FirstHourTimeShiftPrice
        var visitDuration = (endTimeSpan - startTimeSpan).TotalMinutes;
           var timeShiftsIncluded= timeShifts.Where(p => startTimeSpan >= p.StartTime || (startTimeSpan+TimeSpan.FromMinutes(visitDuration))>=p.StartTime).ToList().OrderBy(p=>p.StartTime);
        if (timeShiftsIncluded.Count() >= 2)
        {
            var firstTimeShift = timeShiftsIncluded.Single(p => startTimeSpan >= p.StartTime);
            var secondTimeShift = timeShiftsIncluded.Single(p => firstTimeShift.Id != p.Id);
            var firstVisitPartition = (firstTimeShift.EndTime - startTimeSpan).TotalMinutes;
            var secondVisitPartition = visitDuration - firstVisitPartition;
            //Check every single partition if they in First Hour or second and calc Price
            if(firstVisitPartition-60 > 0) {
                var pricePerMin = firstTimeShift.PriceForRemainingHours / 60;
                var timeInSecondHour = (decimal)firstVisitPartition - 60;
                totalPrice += (timeInSecondHour * pricePerMin) + firstTimeShift.PriceForFirstHour;
            }else
            {
                var pricePerMin = firstTimeShift.PriceForFirstHour / 60;
                totalPrice += (decimal)firstVisitPartition * pricePerMin;
            }
            if (secondVisitPartition - 60 > 0)
            {
                var pricePerMin = secondTimeShift.PriceForRemainingHours / 60;
                var timeInSecondHour = (decimal)secondVisitPartition - 60;
                totalPrice += (timeInSecondHour * pricePerMin) + secondTimeShift.PriceForFirstHour;
            }
            else
            {
                var pricePerMin = secondTimeShift.PriceForFirstHour / 60;
                totalPrice += (decimal)secondVisitPartition * pricePerMin;
            }

        }
        else if(timeShiftsIncluded.Any())
        {
            if (visitDuration - 60 > 0)
            {
                var pricePerMin = timeShiftsIncluded.Single().PriceForRemainingHours / 60;
                var timeInSecondHour = (decimal)visitDuration - 60;
                totalPrice += (timeInSecondHour * pricePerMin) + timeShiftsIncluded.Single().PriceForFirstHour;
            }
            else
            {
                var pricePerMin = timeShiftsIncluded.Single().PriceForFirstHour / 60;
                totalPrice += (decimal)visitDuration * pricePerMin;
            }
        }
        return totalPrice;
    }
}
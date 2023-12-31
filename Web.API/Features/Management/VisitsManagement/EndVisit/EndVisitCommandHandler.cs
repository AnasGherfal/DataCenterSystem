﻿using Core.Constants;
using Core.Entities;
using Core.Events.Visit;
using Core.Exceptions;
using Core.Interfaces.Services;
using Core.Wrappers;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
        var endDate = DateTime.Parse(request.EndTime ?? "");// ?? DateTime.UtcNow);
        if (endDate < data.StartTime) throw new BadRequestException("VISIT_END_TIME_IS_BEFORE_START_TIME");
        if (endDate.Date != data.StartTime.Value.Date)
        {
            var afterEndDayNotStartDay = endDate.Date.AddDays(-1).DayOfWeek != data.StartTime.Value.Date.DayOfWeek;
            if (afterEndDayNotStartDay) throw new BadRequestException("VISIT_DAY_DIFFERENCE_MORE_THAN_ONE_DAY");
        }
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
        //[Edited!] checking visit duration if its Greater than one hour Deal with it
        //& if its less than or equal to one hour even with overlap just take FirstHourTimeShiftPrice
        var visitDuration = (endTimeSpan - startTimeSpan).TotalMinutes;
        if (visitDuration <= 60)
        {
            var timeShiftOfVisit = timeShifts.Single(p => startTimeSpan >= p.StartTime && endTimeSpan <= p.EndTime);
            if (timeShiftOfVisit == null) throw new NotFoundException("Time_Shift_Not_Founded");
            totalPrice += timeShiftOfVisit.PriceForFirstHour;
        }
        else
        {
            //[Edited!]Check if there An OverLap exist before dealing with it
            // checking TimeShifts That Included in the Visit
           var timeShiftsIncluded= timeShifts.Where(p => startTimeSpan >= p.StartTime || (startTimeSpan+TimeSpan.FromMinutes(visitDuration))>=p.StartTime).ToList().OrderBy(p=>p.StartTime);
            if (timeShiftsIncluded.Count() >= 2){
                var firstTimeShift = timeShiftsIncluded.Single(p => startTimeSpan >= p.StartTime);
                var secondTimeShift = timeShiftsIncluded.Single(p => firstTimeShift.Id != p.Id);
                var firstVisitPartation =  (firstTimeShift.EndTime-startTimeSpan).TotalMinutes;
                var secondVisitPartation = visitDuration - firstVisitPartation;
                if (firstVisitPartation <= 60)
                {
                    totalPrice += firstTimeShift.PriceForFirstHour;
                }
                else
                {
                    int hours = (int)(firstVisitPartation / 60);
                    totalPrice += firstTimeShift.PriceForRemainingHours*hours;
                }

                if (secondVisitPartation <= 60)
                {
                    totalPrice += secondTimeShift.PriceForFirstHour;
                }
                else
                {
                    int hours = (int)(secondVisitPartation / 60);
                    totalPrice += secondTimeShift.PriceForRemainingHours*hours;
                }




                /* foreach (var timeShift in timeShiftsIncluded)
                 {

                     var overlapStart = startTimeSpan < timeShift.EndTime ? timeShift.StartTime : startTimeSpan;
                     var overlapEnd = endTimeSpan > timeShift.StartTime ? endTimeSpan : timeShift.EndTime ;
                     var overlapDuration = overlapEnd - overlapStart;
                     if (overlapDuration <= TimeSpan.Zero) continue;
                     var firstHourPrice = timeShift.PriceForFirstHour;
                     var remainingHoursPrice = timeShift.PriceForRemainingHours * (decimal)(overlapDuration.TotalHours - 1);
                     var timeShiftPrice = firstHourPrice + remainingHoursPrice;
                     totalPrice += timeShiftPrice;
                 }*/
            }
            else
            {
                totalPrice += timeShiftsIncluded.Single().PriceForRemainingHours;
            }
        }
        return totalPrice;
    }
}
using Core.Events.TimeShift;

namespace Core.Entities;

public class TimeShift: Entity
{
    public Guid Id { get; set; }
    public DateTime? Date { get; set; }
    public DayOfWeek? Day { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public decimal PriceForFirstHour { get; set; }
    public decimal PriceForRemainingHours { get; set; }

    
    public void Apply(TimeShiftCreatedEvent @event)
    {
        Sequence = @event.Sequence;
        CreatedOn = @event.DateTime;
        UpdatedOn = @event.DateTime;
        Id = @event.AggregateId;
        Date = @event.Data.Date;
        Day = @event.Data.Day;
        StartTime = @event.Data.StartTime;
        EndTime = @event.Data.EndTime;
        PriceForFirstHour = @event.Data.PriceForFirstHour;
        PriceForRemainingHours = @event.Data.PriceForRemainingHours;
    }
    
    public void Apply(TimeShiftUpdatedEvent @event)
    {
        Sequence = @event.Sequence;
        UpdatedOn = @event.DateTime;
        PriceForFirstHour = @event.Data.PriceForFirstHour;
        PriceForRemainingHours = @event.Data.PriceForRemainingHours;
        StartTime = @event.Data.StartTime ?? StartTime;
        EndTime = @event.Data.EndTime ?? EndTime;
    }
    
    public void Apply(TimeShiftDeletedEvent @event)
    {
        Sequence = @event.Sequence;
        UpdatedOn = @event.DateTime;
        IsDeleted = true;
    }
    
}
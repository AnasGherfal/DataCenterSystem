using Infrastructure.Constants;

namespace Web.API.Features.TimeShiftManagement.FetchTimeShifts
{
    public sealed record FetchTimeShiftsQueryResponse
    {
        public Guid Id { get; set; }
        public DateTime? Date { get; set; }
        public DayOfWeek? Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public decimal PriceForFirstHour { get; set; }
        public decimal PriceForRemainingHours { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}

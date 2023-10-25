namespace Web.API.Features.TimeShiftManagement.FetchTimeShiftById
{
    public sealed record FetchTimeShiftByIdQueryResponse
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

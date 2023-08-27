namespace Web.API.Features.AnalyticsManagement.FetchDashboardStatistics
{
    public sealed record FetchDashboardStatisticsQueryResponse
    {
        public int TotalSubscriptions { get; set; }
        public int TotalExpireThisMonth { get; set; }
        public int TotalExpired { get; set; }
        public int TotalVisits { get; set; }
        public int TotalVisitsExpectedThisMonth { get; set; }
        public int TotalOccuring { get; set; }
    }
}

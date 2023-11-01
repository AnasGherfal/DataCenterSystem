namespace Web.API.Features.Consumer.AnalyticsManagement.FetchDashboardStatistics
{
    public sealed record FetchDashboardStatisticsQueryResponse
    {
        public int TotalSubscriptions { get; set; }
        public int TotalExpireThisMonth { get; set; }
        public int TotalExpired { get; set; }
        public int TotalVisits { get; set; }
        public int TotalVisitsRequestedThisMonth { get; set; }
        public int TotalOccuring { get; set; }
    }
}

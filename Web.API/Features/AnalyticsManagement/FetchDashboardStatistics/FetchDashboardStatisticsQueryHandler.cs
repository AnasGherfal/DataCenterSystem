using Core.Dtos;
using Infrastructure;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Features.AnalyticsManagement.FetchDashboardStatistics;

public sealed record FetchDashboardStatisticsQueryHandler : IRequestHandler<FetchDashboardStatisticsQuery, ContentResponse<FetchDashboardStatisticsQueryResponse>>
{
    private readonly AppDbContext _dbContext;

    public FetchDashboardStatisticsQueryHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ContentResponse<FetchDashboardStatisticsQueryResponse>> Handle(
        FetchDashboardStatisticsQuery request, CancellationToken cancellationToken)
    {
        var in30Days = DateTime.UtcNow.AddDays(30);
        var subscriptionCounts = await _dbContext.Subscriptions
            .Select(p => new
            {
                TotalSubscriptions = 1,
                TotalExpireThisMonth = (p.EndDate <= in30Days) ? 1 : 0,
                TotalExpired = (p.EndDate < DateTime.UtcNow) ? 1 : 0
            })
            .GroupBy(p => 1)
            .Select(g => new
            {
                TotalSubscriptions = g.Sum(p => p.TotalSubscriptions),
                TotalExpireThisMonth = g.Sum(p => p.TotalExpireThisMonth),
                TotalExpired = g.Sum(p => p.TotalExpired)
            })
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        var visitsCount = await _dbContext.Visits
            .Select(p => new
            {
                TotalVisits = 1,
                TotalVisitsExpectedThisMonth = (p.ExpectedStartTime <= in30Days) ? 1 : 0,
                TotalOccuring = (p.StartTime != null && p.EndTime == null) ? 1 : 0
            })
            .GroupBy(p => 1)
            .Select(g => new
            {
                TotalVisits = g.Sum(p => p.TotalVisits),
                TotalVisitsExpectedThisMonth = g.Sum(p => p.TotalVisitsExpectedThisMonth),
                TotalOccuring = g.Sum(p => p.TotalOccuring)
            })
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        var result = new FetchDashboardStatisticsQueryResponse()
        {
            TotalSubscriptions = subscriptionCounts?.TotalSubscriptions ?? 0,
            TotalExpireThisMonth = subscriptionCounts?.TotalExpireThisMonth ?? 0,
            TotalExpired = subscriptionCounts?.TotalExpired ?? 0,
            TotalVisits = visitsCount?.TotalVisits ?? 0,
            TotalVisitsExpectedThisMonth = visitsCount?.TotalVisitsExpectedThisMonth ?? 0,
            TotalOccuring = visitsCount?.TotalOccuring ?? 0
        };
        return new ContentResponse<FetchDashboardStatisticsQueryResponse>("", result);
    }
}
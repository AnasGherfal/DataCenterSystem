using Core.Wrappers;
using MediatR;

namespace Web.API.Features.AnalyticsManagement.FetchDashboardStatistics;

public sealed record FetchDashboardStatisticsQuery : IRequest<ContentResponse<FetchDashboardStatisticsQueryResponse>>
{
}
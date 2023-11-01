using Core.Wrappers;
using MediatR;

namespace Web.API.Features.Management.AnalyticsManagement.FetchDashboardStatistics;

public sealed record FetchDashboardStatisticsQuery : IRequest<ContentResponse<FetchDashboardStatisticsQueryResponse>>
{
}
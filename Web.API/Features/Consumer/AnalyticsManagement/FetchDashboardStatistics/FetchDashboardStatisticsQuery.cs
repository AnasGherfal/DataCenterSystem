using Core.Wrappers;
using MediatR;

namespace Web.API.Features.Consumer.AnalyticsManagement.FetchDashboardStatistics;

public sealed record FetchDashboardStatisticsQuery : IRequest<ContentResponse<FetchDashboardStatisticsQueryResponse>>
{
}
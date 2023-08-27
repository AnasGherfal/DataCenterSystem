using Core.Dtos;
using MediatR;

namespace Web.API.Features.AnalyticsManagement.FetchDashboardStatistics;

public sealed record FetchDashboardStatisticsQuery : IRequest<ContentResponse<FetchDashboardStatisticsQueryResponse>>
{
}
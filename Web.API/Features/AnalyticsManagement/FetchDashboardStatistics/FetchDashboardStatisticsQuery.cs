using MediatR;
using Shared.Dtos;

namespace Web.API.Features.AnalyticsManagement.FetchDashboardStatistics;

public sealed record FetchDashboardStatisticsQuery : IRequest<ContentResponse<FetchDashboardStatisticsQueryResponse>>
{
}
using Core.Interfaces.Services;
using Core.Wrappers;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Features.Consumer.VisitsManagement.FetchMyVisits;

public sealed record FetchMyVisitsQueryHandler : IRequestHandler<FetchMyVisitsQuery, PagedResponse<FetchMyVisitsQueryResponse>>
{
    private readonly AppDbContext _dbContext;
    private readonly IClientService _client;

    public FetchMyVisitsQueryHandler(AppDbContext dbContext, IClientService client)
    {
        _dbContext = dbContext;
        _client = client;
    }

    public async Task<PagedResponse<FetchMyVisitsQueryResponse>> Handle(FetchMyVisitsQuery request, CancellationToken cancellationToken)
    {
        var pageNumber = request.PageNumber ?? 1;
        var pageSize = request.PageSize ?? 5;
        var query = _dbContext.Visits
            .Where(p => string.IsNullOrWhiteSpace(request.SubscriptionId) || p.SubscriptionId == Guid.Parse(request.SubscriptionId!))
            .Where(p => p.CustomerId == _client.GetIdentifier());
        var data = await query
            .OrderBy(p => p.CreatedOn)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .Select(p =>  new FetchMyVisitsQueryResponse()
            {
                Id = p.Id,
                ExpectedStartTime = p.ExpectedStartTime,
                ExpectedEndTime = p.ExpectedEndTime,
                TotalMinutes = p.TotalTime,
                Price = p.VisitPrice,
                Notes = p.Notes ?? "",
                VisitType = p.VisitType,
                VisitStatus = GetStatus(p.StartTime, p.EndTime),
                CreatedOn = p.CreatedOn,
            })
            .ToListAsync(cancellationToken: cancellationToken);
        var count = await query.CountAsync(cancellationToken: cancellationToken);
        return new PagedResponse<FetchMyVisitsQueryResponse>("", data, count, pageNumber, pageSize);
    }

    public static string GetStatus(DateTime? startTime, DateTime? endTime)
    {
        if (startTime == null & endTime == null) return "Not Started";
        if (startTime != null & endTime == null) return "In Progress";
        if (startTime != null & endTime != null) return "Completed";
        return "Not Started";
    }
}
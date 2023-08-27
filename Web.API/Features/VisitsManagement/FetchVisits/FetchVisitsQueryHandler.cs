using Core.Wrappers;
using Infrastructure;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Features.VisitsManagement.FetchVisits;

public sealed record FetchVisitsQueryHandler : IRequestHandler<FetchVisitsQuery, PagedResponse<FetchVisitsQueryResponse>>
{
    private readonly AppDbContext _dbContext;

    public FetchVisitsQueryHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResponse<FetchVisitsQueryResponse>> Handle(FetchVisitsQuery request, CancellationToken cancellationToken)
    {
        var pageNumber = request.PageNumber ?? 1;
        var pageSize = request.PageSize ?? 5;
        var query = _dbContext.Visits
            .Where(p => string.IsNullOrWhiteSpace(request.SubscriptionId) || p.SubscriptionId == Guid.Parse(request.SubscriptionId!))
            .Where(p => string.IsNullOrWhiteSpace(request.CustomerId) || p.CustomerId == Guid.Parse(request.CustomerId!));
        var data = await query
            .Include(p => p.Customer)
            .OrderBy(p => p.CreatedOn)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .Select(p =>  new FetchVisitsQueryResponse()
            {
                Id = p.Id,
                CustomerName = p.Customer!.Name,
                ExpectedStartTime = p.ExpectedStartTime,
                ExpectedEndTime = p.ExpectedEndTime,
                TotalMinutes = p.TotalTime,
                Price = p.VisitPrice,
                Notes = p.Notes ?? "",
                VisitType = p.VisitType,
                CreatedOn = p.CreatedOn,
            })
            .ToListAsync(cancellationToken: cancellationToken);
        var count = await query.CountAsync(cancellationToken: cancellationToken);
        return new PagedResponse<FetchVisitsQueryResponse>("", data, count, pageNumber, pageSize);
    }
}
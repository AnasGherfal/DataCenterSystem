using Core.Constants;
using Core.Wrappers;
using Infrastructure;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Features.SubscriptionManagement.FetchSubscriptions;

public sealed record FetchSubscriptionsQueryHandler : IRequestHandler<FetchSubscriptionsQuery, PagedResponse<FetchSubscriptionsQueryResponse>>
{
    private readonly AppDbContext _dbContext;

    public FetchSubscriptionsQueryHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResponse<FetchSubscriptionsQueryResponse>> Handle(FetchSubscriptionsQuery request, CancellationToken cancellationToken)
    {
        var pageNumber = request.PageNumber ?? 1;
        var pageSize = request.PageSize ?? 5;
        var query = _dbContext.Subscriptions
            .Where(p => string.IsNullOrWhiteSpace(request.CustomerId) || p.CustomerId == Guid.Parse(request.CustomerId!));
        switch (request.Status)
        {
            case SubscriptionStatus.Active:
                query = query.Where(p => p.EndDate > DateTime.Now);
                break;
            case SubscriptionStatus.ExpireIn30Days:
                query = query.Where(p => p.EndDate > DateTime.Now && p.EndDate < DateTime.Now.AddDays(30));
                break;
            case SubscriptionStatus.Expired:
                query = query.Where(p => p.EndDate < DateTime.Now);
                break;
        }
        var data = await query.Include(p => p.Customer)
            .Include(p => p.Service)
            .OrderBy(p => p.CreatedOn)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .Select(p =>  new FetchSubscriptionsQueryResponse()
            {
                Id = p.Id,
                CustomerName = p.Customer!.Name,
                ServiceName = p.Service!.Name,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                TotalPrice = p.TotalPrice,
                Status = p.Status,
                CreatedOn = p.CreatedOn,
            })
            .ToListAsync(cancellationToken: cancellationToken);
        var count = await query.CountAsync(cancellationToken: cancellationToken);
        return new PagedResponse<FetchSubscriptionsQueryResponse>("", data, count, pageNumber, pageSize);
    }
}
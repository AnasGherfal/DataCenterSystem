using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;

namespace Web.API.Features.SubscriptionManagement.FetchSubscriptions;

public sealed record FetchSubscriptionsQueryHandler : IRequestHandler<FetchSubscriptionsQuery, PagedResponse<FetchSubscriptionsQueryResponse>>
{
    private readonly DataCenterContext _dbContext;

    public FetchSubscriptionsQueryHandler(DataCenterContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResponse<FetchSubscriptionsQueryResponse>> Handle(FetchSubscriptionsQuery request, CancellationToken cancellationToken)
    {
        var pageNumber = request.PageNumber ?? 1;
        var pageSize = request.PageSize ?? 5;
        var data = await _dbContext.Subscriptions
            .Include(p => p.Customer)
            .Include(p => p.Service)
            .Where(p => string.IsNullOrWhiteSpace(request.CustomerId) || p.CustomerId == Guid.Parse(request.CustomerId!))
            .OrderBy(p => p.CreatedOn)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .Select(p =>  new FetchSubscriptionsQueryResponse()
            {
                Id = p.Id,
                CustomerName = p.Customer.Name,
                ServiceName = p.Service.Name,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                TotalPrice = p.TotalPrice ?? 0,
                Status = p.Status,
                CreatedOn = p.CreatedOn,
            })
            .ToListAsync(cancellationToken: cancellationToken);
        var count = await _dbContext.Users.CountAsync(cancellationToken: cancellationToken);
        return new PagedResponse<FetchSubscriptionsQueryResponse>("", data, count, pageNumber, pageSize);
    }
}
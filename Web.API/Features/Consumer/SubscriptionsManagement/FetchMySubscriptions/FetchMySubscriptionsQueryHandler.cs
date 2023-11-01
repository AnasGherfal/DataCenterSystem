using Core.Constants;
using Core.Interfaces.Services;
using Core.Wrappers;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Features.Consumer.SubscriptionsManagement.FetchMySubscriptions;

public sealed record FetchMySubscriptionsQueryHandler : IRequestHandler<FetchMySubscriptionsQuery, PagedResponse<FetchMySubscriptionsQueryResponse>>
{
    private readonly AppDbContext _dbContext;
    private readonly IClientService _client;

    public FetchMySubscriptionsQueryHandler(AppDbContext dbContext, IClientService client)
    {
        _dbContext = dbContext;
        _client = client;
    }

    public async Task<PagedResponse<FetchMySubscriptionsQueryResponse>> Handle(FetchMySubscriptionsQuery request, CancellationToken cancellationToken)
    {
        var pageNumber = request.PageNumber ?? 1;
        var pageSize = request.PageSize ?? 5;
        var query = _dbContext.Subscriptions
            .Where(p => p.CustomerId == _client.GetIdentifier());
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
        var data = await query
            .Include(p => p.Service)
            .OrderBy(p => p.CreatedOn)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .Select(p =>  new FetchMySubscriptionsQueryResponse()
            {
                Id = p.Id,
                ServiceName = p.Service!.Name,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                TotalPrice = p.TotalPrice,
                Status = p.Status,
                CreatedOn = p.CreatedOn,
            })
            .ToListAsync(cancellationToken: cancellationToken);
        var count = await query.CountAsync(cancellationToken: cancellationToken);
        return new PagedResponse<FetchMySubscriptionsQueryResponse>("", data, count, pageNumber, pageSize);
    }
}
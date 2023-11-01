using Core.Exceptions;
using Core.Interfaces.Services;
using Core.Wrappers;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Features.Consumer.SubscriptionsManagement.FetchMySubscriptionById;

public sealed record FetchMySubscriptionByIdQueryHandler : IRequestHandler<FetchMySubscriptionByIdQuery, ContentResponse<FetchMySubscriptionByIdQueryResponse>>
{
    private readonly AppDbContext _dbContext;
    private readonly IClientService _client;

    public FetchMySubscriptionByIdQueryHandler(AppDbContext dbContext, IClientService client)
    {
        _dbContext = dbContext;
        _client = client;
    }

    public async Task<ContentResponse<FetchMySubscriptionByIdQueryResponse>> Handle(FetchMySubscriptionByIdQuery request, CancellationToken cancellationToken)
    {
        var data = await _dbContext.Subscriptions
            .Include(p => p.Customer)
            .Include(p => p.Service)
            .Include(p => p.Documents)
            .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id!)
                && p.CustomerId == _client.GetIdentifier(), cancellationToken: cancellationToken);
        if (data == null) throw new NotFoundException("SUBSCRIPTION_NOT_FOUND");
        return new ContentResponse<FetchMySubscriptionByIdQueryResponse>("", new FetchMySubscriptionByIdQueryResponse()
        {
            Id = data.Id,
            CustomerName = data.Customer!.Name,
            ServiceName = data.Service!.Name,
            StartDate = data.StartDate,
            EndDate = data.EndDate,
            TotalPrice = data.TotalPrice,
            Status = data.Status,
            CreatedOn = data.CreatedOn,
            Files = data.Documents
                .Select(p => new FetchMySubscriptionByIdQueryResponseItem()
                {
                    Id = p.Id,
                    FileType = p.FileType,
                    IsActive = p.IsActive,
                    CreatedOn = p.CreatedOn, 
                }).ToList(),
        });
    }
}
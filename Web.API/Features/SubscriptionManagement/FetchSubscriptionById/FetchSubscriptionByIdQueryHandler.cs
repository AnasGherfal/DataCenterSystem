using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Exceptions;

namespace Web.API.Features.SubscriptionManagement.FetchSubscriptionById;

public sealed record FetchSubscriptionByIdQueryHandler : IRequestHandler<FetchSubscriptionByIdQuery, ContentResponse<FetchSubscriptionByIdQueryResponse>>
{
    private readonly DataCenterContext _dbContext;

    public FetchSubscriptionByIdQueryHandler(DataCenterContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ContentResponse<FetchSubscriptionByIdQueryResponse>> Handle(FetchSubscriptionByIdQuery request, CancellationToken cancellationToken)
    {
        var data = await _dbContext.Subscriptions
            .Include(p => p.Customer)
            .Include(p => p.Service)
            .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id!), cancellationToken: cancellationToken);
        if (data == null) throw new NotFoundException("SUBSCRIPTION_NOT_FOUND");
        return new ContentResponse<FetchSubscriptionByIdQueryResponse>("", new FetchSubscriptionByIdQueryResponse()
        {
            Id = data.Id,
            CustomerName = data.Customer.Name,
            ServiceName = data.Service.Name,
            StartDate = data.StartDate,
            EndDate = data.EndDate,
            TotalPrice = data.TotalPrice ?? 0,
            Status = data.Status,
            CreatedOn = data.CreatedOn,
            Files = { 
                new FetchSubscriptionByIdQueryResponseItem() 
                {
                    Id = data.SubscriptionFile.Id,
                    FileName = data.SubscriptionFile.FileName,
                    FileType = data.SubscriptionFile.FileType,
                    Status = data.SubscriptionFile.IsActive,
                    CreatedOn = data.SubscriptionFile.CreatedOn,
                },
            }
        });
    }
}
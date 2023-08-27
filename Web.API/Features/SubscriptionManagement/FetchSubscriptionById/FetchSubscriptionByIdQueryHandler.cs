using Core.Dtos;
using Core.Exceptions;
using Infrastructure;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Features.SubscriptionManagement.FetchSubscriptionById;

public sealed record FetchSubscriptionByIdQueryHandler : IRequestHandler<FetchSubscriptionByIdQuery, ContentResponse<FetchSubscriptionByIdQueryResponse>>
{
    private readonly AppDbContext _dbContext;

    public FetchSubscriptionByIdQueryHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ContentResponse<FetchSubscriptionByIdQueryResponse>> Handle(FetchSubscriptionByIdQuery request, CancellationToken cancellationToken)
    {
        var data = await _dbContext.Subscriptions
            .Include(p => p.Customer)
            .Include(p => p.Service)
            .Include(p => p.Documents)
            .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id!), cancellationToken: cancellationToken);
        if (data == null) throw new NotFoundException("SUBSCRIPTION_NOT_FOUND");
        var document = data.Documents.FirstOrDefault(p => p.IsActive);
        return new ContentResponse<FetchSubscriptionByIdQueryResponse>("", new FetchSubscriptionByIdQueryResponse()
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
                .Select(p => new FetchSubscriptionByIdQueryResponseItem()
                {
                    Id = p.Id,
                    FileType = p.FileType,
                    IsActive = p.IsActive,
                    CreatedOn = p.CreatedOn, 
                }).ToList(),
        });
    }
}
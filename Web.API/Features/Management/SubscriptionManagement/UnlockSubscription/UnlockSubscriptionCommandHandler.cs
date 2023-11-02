using Core.Constants;
using Core.Events.Subscription;
using Core.Exceptions;
using Core.Interfaces.Services;
using Core.Wrappers;
using Infrastructure;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Features.SubscriptionManagement.UnlockSubscription;

public sealed record UnlockSubscriptionCommandHandler : IRequestHandler<UnlockSubscriptionCommand, MessageResponse>
{
    private readonly IClientService _client;
    private readonly AppDbContext _dbContext;

    public UnlockSubscriptionCommandHandler(AppDbContext dbContext, IClientService client)
    {
        _dbContext = dbContext;
        _client = client;
    }

    public async Task<MessageResponse> Handle(UnlockSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.Parse(request.Id!);
        var data = await _dbContext.Subscriptions.SingleOrDefaultAsync(p => p.Id == id, cancellationToken: cancellationToken);
        if (data == null) throw new NotFoundException("subscription not found");
        if (data.Status != GeneralStatus.Locked) throw new BadRequestException("subscription is not locked");
        var customerIsActive = await _dbContext.Customers
            .AnyAsync(p => p.Id == data.CustomerId
                           && p.Status == GeneralStatus.Active, cancellationToken: cancellationToken);
        if (!customerIsActive) throw new BadRequestException("العميل غير موجود");
        var @event = new SubscriptionUnlockedEvent(_client.GetIdentifier(), data.Id, data.Sequence + 1, new SubscriptionUnlockedEventData());
        data.Apply(@event);
        _dbContext.Entry(data).State = EntityState.Modified;
        await _dbContext.Events.AddAsync(@event, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new MessageResponse()
        {
            Msg = "subscription unlocked successfully!"
        };
    }
}
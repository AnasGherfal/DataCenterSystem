using Core.Constants;
using Core.Dtos;
using Core.Events.Subscription;
using Core.Exceptions;
using Infrastructure;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.API.Services.ClientService;

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
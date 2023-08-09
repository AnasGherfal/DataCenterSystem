using Infrastructure;
using Infrastructure.Audits.Service;
using Infrastructure.Audits.Subscription;
using Infrastructure.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Exceptions;

namespace Web.API.Features.SubscriptionManagement.UnlockSubscription;

public sealed record UnlockSubscriptionCommandHandler : IRequestHandler<UnlockSubscriptionCommand, MessageResponse>
{
    private readonly DataCenterContext _dbContext;

    public UnlockSubscriptionCommandHandler(DataCenterContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<MessageResponse> Handle(UnlockSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.Parse(request.Id!);
        var data = await _dbContext.Subscriptions.SingleOrDefaultAsync(p => p.Id == id && p.Status != GeneralStatus.Deleted, cancellationToken: cancellationToken);
        if (data == null) throw new NotFoundException("subscription not found");
        if (data.Status != GeneralStatus.Locked) throw new BadRequestException("subscription is not locked");
        var @event = new SubscriptionUnlockedAudit("", Guid.NewGuid(), new SubscriptionUnlockedAuditData());
        data.Apply(@event);
        _dbContext.Entry(data).State = EntityState.Modified;
        await _dbContext.Audits.AddAsync(@event, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new MessageResponse()
        {
            Msg = "subscription unlocked successfully!"
        };
    }
}
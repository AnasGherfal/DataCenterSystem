using Infrastructure;
using Infrastructure.Audits.Service;
using Infrastructure.Audits.Subscription;
using Infrastructure.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Exceptions;

namespace Web.API.Features.SubscriptionManagement.LockSubscription;

public sealed record LockSubscriptionCommandHandler : IRequestHandler<LockSubscriptionCommand, MessageResponse>
{
    private readonly DataCenterContext _dbContext;

    public LockSubscriptionCommandHandler(DataCenterContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<MessageResponse> Handle(LockSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.Parse(request.Id!);
        var data = await _dbContext.Subscriptions.SingleOrDefaultAsync(p => p.Id == id && p.Status != GeneralStatus.Deleted, cancellationToken: cancellationToken);
        if (data == null) throw new NotFoundException("Subscription not found");
        if (data.Status == GeneralStatus.Locked) throw new BadRequestException("Sorry, this subscription is already locked");
        if (data.Status != GeneralStatus.Active) throw new BadRequestException("Sorry, this cannot be locked");
        var @event = new SubscriptionLockedAudit("", Guid.NewGuid(), new SubscriptionLockedAuditData());
        data.Apply(@event);
        _dbContext.Entry(data).State = EntityState.Modified;
        await _dbContext.Audits.AddAsync(@event, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new MessageResponse()
        {
            Msg = "Subscription locked successfully!",
        };
    }
}
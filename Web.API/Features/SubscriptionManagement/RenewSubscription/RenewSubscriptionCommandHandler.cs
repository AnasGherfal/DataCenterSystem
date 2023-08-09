using Infrastructure;
using Infrastructure.Audits.Subscription;
using Infrastructure.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Exceptions;

namespace Web.API.Features.SubscriptionManagement.RenewSubscription;

public sealed record RenewSubscriptionCommandHandler : IRequestHandler<RenewSubscriptionCommand, MessageResponse>
{
    private readonly DataCenterContext _dbContext;

    public RenewSubscriptionCommandHandler(DataCenterContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<MessageResponse> Handle(RenewSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.Parse(request.Id!);
        var data = await _dbContext.Subscriptions.SingleOrDefaultAsync(p => p.Id == id && p.Status != GeneralStatus.Deleted, cancellationToken: cancellationToken);
        if (data == null) throw new NotFoundException("Subscription not found");
        if (data.Status != GeneralStatus.Active) throw new BadRequestException("Sorry, this subscription is not active");
        if (data.EndDate > DateTime.UtcNow.AddDays(-30)) throw new BadRequestException("Sorry, this subscription can be renewed only within 30days");
        var @event = new SubscriptionRenewedAudit("", Guid.NewGuid(), new SubscriptionRenewedAuditData());
        data.Apply(@event);
        _dbContext.Entry(data).State = EntityState.Modified;
        await _dbContext.Audits.AddAsync(@event, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new MessageResponse()
        {
            Msg = "Subscription renewed successfully!",
        };
    }
}
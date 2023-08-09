using Infrastructure;
using Infrastructure.Audits.Service;
using Infrastructure.Audits.Subscription;
using Infrastructure.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Exceptions;

namespace Web.API.Features.SubscriptionManagement.DeleteSubscription;

public sealed record DeleteSubscriptionCommandHandler : IRequestHandler<DeleteSubscriptionCommand, MessageResponse>
{
    private readonly DataCenterContext _dbContext;

    public DeleteSubscriptionCommandHandler(DataCenterContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<MessageResponse> Handle(DeleteSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.Parse(request.Id!);
        var data = await _dbContext.Subscriptions
            .Include(p => p.SubscriptionFile)
            .SingleOrDefaultAsync(p => p.Id == id && p.Status != GeneralStatus.Deleted, cancellationToken: cancellationToken);
        if (data == null) throw new NotFoundException("subscription not found");
        var @event = new SubscriptionDeletedAudit("", Guid.NewGuid(), new SubscriptionDeletedAuditData());
        data.Apply(@event);
        _dbContext.Entry(data).State = EntityState.Modified;
        await _dbContext.Audits.AddAsync(@event, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new MessageResponse()
        {
            Msg = "تمت حدف بنجاح",
        };
    }
}
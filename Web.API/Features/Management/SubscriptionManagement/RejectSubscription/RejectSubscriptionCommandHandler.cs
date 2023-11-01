using Core.Constants;
using Core.Events.Subscription;
using Core.Exceptions;
using Core.Interfaces.Services;
using Core.Wrappers;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Features.Management.SubscriptionManagement.RejectSubscription;

public sealed record RejectSubscriptionCommandHandler : IRequestHandler<RejectSubscriptionCommand, MessageResponse>
{
    private readonly IClientService _client;
    private readonly AppDbContext _dbContext;

    public RejectSubscriptionCommandHandler(AppDbContext dbContext, IClientService client)
    {
        _dbContext = dbContext;
        _client = client;
    }

    public async Task<MessageResponse> Handle(RejectSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.Parse(request.Id!);
        var data = await _dbContext.Subscriptions.SingleOrDefaultAsync(p => p.Id == id, cancellationToken: cancellationToken);
        if (data == null) throw new NotFoundException("Subscription not found");
        if (data.Status != GeneralStatus.Requested) throw new BadRequestException("Sorry, this cannot be approved.");
        var @event = new SubscriptionRejectedEvent(_client.GetIdentifier(), data.Id, data.Sequence + 1, new SubscriptionRejectedEventData());
        data.Apply(@event);
        _dbContext.Entry(data).State = EntityState.Modified;
        await _dbContext.Events.AddAsync(@event, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new MessageResponse()
        {
            Msg = "Subscription Rejected successfully!",
        };
    }
}
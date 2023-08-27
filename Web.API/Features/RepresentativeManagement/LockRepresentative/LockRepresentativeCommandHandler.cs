using Core.Constants;
using Core.Dtos;
using Core.Events.Subscription;
using Core.Exceptions;
using Infrastructure;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.API.Services.ClientService;

namespace Web.API.Features.RepresentativeManagement.LockRepresentative;

public sealed record LockRepresentativeCommandHandler : IRequestHandler<LockRepresentativeCommand, MessageResponse>
{
    private readonly IClientService _client;
    private readonly AppDbContext _dbContext;

    public LockRepresentativeCommandHandler(AppDbContext dbContext, IClientService client)
    {
        _dbContext = dbContext;
        _client = client;
    }

    public async Task<MessageResponse> Handle(LockRepresentativeCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.Parse(request.Id!);
        var data = await _dbContext.Subscriptions.SingleOrDefaultAsync(p => p.Id == id, cancellationToken: cancellationToken);
        if (data == null) throw new NotFoundException("Subscription not found");
        if (data.Status == GeneralStatus.Locked) throw new BadRequestException("Sorry, this subscription is already locked");
        if (data.Status != GeneralStatus.Active) throw new BadRequestException("Sorry, this cannot be locked");
        var @event = new SubscriptionLockedEvent(_client.GetIdentifier(), data.Id, data.Sequence + 1, new SubscriptionLockedEventData());
        data.Apply(@event);
        _dbContext.Entry(data).State = EntityState.Modified;
        await _dbContext.Events.AddAsync(@event, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new MessageResponse()
        {
            Msg = "Subscription locked successfully!",
        };
    }
}
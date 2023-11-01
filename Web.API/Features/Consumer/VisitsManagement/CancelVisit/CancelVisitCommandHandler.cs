using Core.Events.Visit;
using Core.Exceptions;
using Core.Interfaces.Services;
using Core.Wrappers;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Features.Consumer.VisitsManagement.CancelVisit;

public sealed record CancelVisitCommandHandler : IRequestHandler<CancelVisitCommand, MessageResponse>
{
    private readonly IClientService _client;
    private readonly AppDbContext _dbContext;

    public CancelVisitCommandHandler(AppDbContext dbContext, IClientService client)
    {
        _dbContext = dbContext;
        _client = client;
    }

    public async Task<MessageResponse> Handle(CancelVisitCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.Parse(request.Id!);
        var data = await _dbContext.Visits.SingleOrDefaultAsync(p => p.Id == id
            && p.CustomerId == _client.GetIdentifier(), cancellationToken: cancellationToken);
        if (data == null) throw new NotFoundException("VISIT_NOT_FOUND");
        if (data.StartTime != null && data.EndTime == null) throw new NotFoundException("VISIT_ALREADY_SIGNED");
        var @event = new VisitCancelledEvent(_client.GetIdentifier(), data.Id, data.Sequence + 1, new VisitCancelledEventData());
        data.Apply(@event);
        _dbContext.Entry(data).State = EntityState.Modified;
        await _dbContext.Events.AddAsync(@event, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new MessageResponse()
        {
            Msg = "VISIT_CANCELLED"
        };
    }
}
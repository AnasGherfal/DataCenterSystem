using Core.Constants;
using Core.Dtos;
using Core.Events.Visit;
using Core.Exceptions;
using Infrastructure;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.API.Services.ClientService;

namespace Web.API.Features.VisitsManagement.StartVisit;

public sealed record StartVisitCommandHandler : IRequestHandler<StartVisitCommand, MessageResponse>
{
    private readonly IClientService _client;
    private readonly AppDbContext _dbContext;

    public StartVisitCommandHandler(AppDbContext dbContext, IClientService client)
    {
        _dbContext = dbContext;
        _client = client;
    }

    public async Task<MessageResponse> Handle(StartVisitCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.Parse(request.Id!);
        var data = await _dbContext.Visits
                       .SingleOrDefaultAsync(p => p.Id == id, cancellationToken: cancellationToken);
        if (data == null) throw new NotFoundException("VISIT_NOT_FOUND");
        if (data.Status == GeneralStatus.Locked) throw new BadRequestException("VISIT_LOCKED");
        if (data.StartTime != null) throw new BadRequestException("VISIT_ALREADY_STARTED");
        var @event = new VisitStartedEvent(_client.GetIdentifier(), data.Id, data.Sequence + 1, new VisitStartedEventData()
        {
            StartTime = request.StartTime?.ToUniversalTime() ?? DateTime.UtcNow,
        });
        data.Apply(@event);
        _dbContext.Entry(data).State = EntityState.Modified;
        await _dbContext.Events.AddAsync(@event, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new MessageResponse()
        {
            Msg = "VISIT_STARTED",
        };
    }
}
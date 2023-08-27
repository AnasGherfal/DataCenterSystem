﻿using Infrastructure;
using Infrastructure.Events.TimeShift;
using Infrastructure.Events.Visit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Exceptions;
using Web.API.Services.ClientService;

namespace Web.API.Features.VisitsManagement.DeleteVisit;

public sealed record DeleteVisitCommandHandler : IRequestHandler<DeleteVisitCommand, MessageResponse>
{
    private readonly IClientService _client;
    private readonly AppDbContext _dbContext;

    public DeleteVisitCommandHandler(AppDbContext dbContext, IClientService client)
    {
        _dbContext = dbContext;
        _client = client;
    }

    public async Task<MessageResponse> Handle(DeleteVisitCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.Parse(request.Id!);
        var data = await _dbContext.Visits.SingleOrDefaultAsync(p => p.Id == id, cancellationToken: cancellationToken);
        if (data == null) throw new NotFoundException("VISIT_NOT_FOUND");
        if (data.StartTime != null) throw new NotFoundException("VISIT_ALREADY_STARTED_END_IT_FIRST");
        var @event = new VisitDeletedEvent(_client.GetIdentifier(), data.Id, data.Sequence + 1, new VisitDeletedEventData());
        data.Apply(@event);
        _dbContext.Entry(data).State = EntityState.Modified;
        await _dbContext.Events.AddAsync(@event, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new MessageResponse()
        {
            Msg = "تمت حدف بنجاح",
        };
    }
}
﻿using Infrastructure;
using Infrastructure.Constants;
using Infrastructure.Entities;
using Infrastructure.Events.TimeShift;
using Infrastructure.Events.Visit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Exceptions;
using Web.API.Services.ClientService;

namespace Web.API.Features.VisitsManagement.CreateVisit;

public sealed record CreateVisitCommandHandler : IRequestHandler<CreateVisitCommand, MessageResponse>
{
    private readonly IClientService _client;
    private readonly AppDbContext _dbContext;

    public CreateVisitCommandHandler(AppDbContext dbContext, IClientService client)
    {
        _dbContext = dbContext;
        _client = client;
    }
    
    public async Task<MessageResponse> Handle(CreateVisitCommand request, CancellationToken cancellationToken)
    {
        var subscriptionId = Guid.Parse(request.SubscriptionId!);
        var subscription = await _dbContext.Subscriptions
            .Where(p => p.Id == subscriptionId)
            .Select(p => new
            {
                p.Id, p.CustomerId, p.Status
            })
            .FirstAsync(cancellationToken: cancellationToken);
        if (subscription == null) throw new NotFoundException("SUBSCRIPTION_NOT_FOUND");
        if (subscription.Status != GeneralStatus.Active) throw new NotFoundException("SUBSCRIPTION_NOT_ACTIVE");
        var representatives = await _dbContext.Representatives
            .Where(p => p.CustomerId == subscription.CustomerId && p.Status == GeneralStatus.Active)
            .Select(p => p.Id)
            .ToListAsync(cancellationToken: cancellationToken);
        var representativesList = request.Representatives!.Select(Guid.Parse).ToList();
        if (representativesList.Except(representatives).Any()) 
            throw new NotFoundException("REPRESENTATIVE_NOT_FOUND");
        var @event = new VisitCreatedEvent(_client.GetIdentifier(), Guid.NewGuid(), new VisitCreatedEventData()
        { 
            CustomerId = subscription.CustomerId,
            SubscriptionId = subscription.Id,
            VisitType = request.VisitType!.Value,
            ExpectedStartTime = request.ExpectedStartTime!.Value,
            ExpectedEndTime = request.ExpectedEndTime!.Value,
            Notes = request.Notes ?? "",
            Representatives = representativesList,
            Companions = request.Companions.Select(p => new VisitCreatedEventDataExtra()
            {
                FirstName = p.FirstName!,
                LastName = p.LastName!,
                IdentityNo = p.IdentityNo!,
                IdentityType = p.IdentityType!.Value,
                JobTitle = p.JobTitle!,
            }).ToList(),
        });
        var data = new Visit(); 
        data.Apply(@event);
        await _dbContext.Visits.AddAsync(data, cancellationToken);
        await _dbContext.Events.AddAsync(@event, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new MessageResponse()
        {
            Msg = "VISIT_CREATED",
        };
    }
}
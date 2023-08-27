using Infrastructure;
using Infrastructure.Constants;
using Infrastructure.Entities;
using Infrastructure.Events.Service;
using Infrastructure.Events.TimeShift;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Exceptions;
using Web.API.Services.ClientService;

namespace Web.API.Features.TimeShiftManagement.CreateTimeShift;

public sealed record CreateTimeShiftCommandHandler : IRequestHandler<CreateTimeShiftCommand, MessageResponse>
{
    private readonly IClientService _client;
    private readonly AppDbContext _dbContext;

    public CreateTimeShiftCommandHandler(AppDbContext dbContext, IClientService client)
    {
        _dbContext = dbContext;
        _client = client;
    }
    
    public async Task<MessageResponse> Handle(CreateTimeShiftCommand request, CancellationToken cancellationToken)
    {
        var isDaySchedule = request.Day != null;
        if (isDaySchedule)
        {
            var overlappingShiftExists = await _dbContext.TimeShifts
                .AnyAsync(p => p.Day == request.Day
                               && ((request.StartTime >= p.StartTime && request.StartTime <= p.EndTime) 
                                   || (request.EndTime >= p.StartTime && request.EndTime <= p.EndTime))
                               || (request.StartTime <= p.StartTime && request.EndTime >= p.EndTime)
                    , cancellationToken: cancellationToken);
            if (overlappingShiftExists) throw new BadRequestException("TIME_SHIFT_ALREADY_EXISTS_OR_OVERLAPS");
        }
        else
        {
            var specialDateExist = await _dbContext.TimeShifts
                .AnyAsync(p => p.Date == request.Date!.Value.Date, cancellationToken: cancellationToken);
            if (specialDateExist) throw new BadRequestException("TIME_SHIFT_ALREADY_EXISTS");
        }
        var @event = new TimeShiftCreatedEvent(_client.GetIdentifier(), Guid.NewGuid(), new TimeShiftCreatedEventData()
        {
            Day = isDaySchedule ? request.Day : null,
            Date = isDaySchedule ? null : request.Date!.Value.Date,
            StartTime = request.StartTime!.Value,
            EndTime = request.EndTime!.Value,
            PriceForFirstHour = request.PriceForFirstHour!.Value,
            PriceForRemainingHours = request.PriceForRemainingHours!.Value,
        });
        var data = new TimeShift();
        data.Apply(@event);
        await _dbContext.TimeShifts.AddAsync(data, cancellationToken);
        await _dbContext.Events.AddAsync(@event, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new MessageResponse()
        {
            Msg = "TIME_SHIFT_CREATED_SUCCESSFULLY",
        };
    }
}
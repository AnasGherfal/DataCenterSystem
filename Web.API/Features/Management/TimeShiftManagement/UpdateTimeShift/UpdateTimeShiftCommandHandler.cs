using Core.Events.TimeShift;
using Core.Exceptions;
using Core.Interfaces.Services;
using Core.Wrappers;
using Infrastructure;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Features.TimeShiftManagement.UpdateTimeShift;

public sealed record UpdateTimeShiftCommandHandler : IRequestHandler<UpdateTimeShiftCommand, MessageResponse>
{
    private readonly IClientService _client;
    private readonly AppDbContext _dbContext;

    public UpdateTimeShiftCommandHandler(AppDbContext dbContext, IClientService client)
    {
        _dbContext = dbContext;
        _client = client;
    }

    public async Task<MessageResponse> Handle(UpdateTimeShiftCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.Parse(request.Id!);
        var data = await _dbContext.TimeShifts
                       .SingleOrDefaultAsync(p => p.Id == id, cancellationToken: cancellationToken);
        if (data == null) throw new NotFoundException("TIME_SHIFT_NOT_FOUND");
        if (data.Date != null && (request.StartTime != null || request.EndTime != null)) throw new NotFoundException("CANNOT_UPDATE_TIME_FOR_DAY_SHIFT");
        if (!(request.StartTime == data.StartTime && request.EndTime == data.EndTime))
        {
            if (data.Day != null && request.StartTime != null)
            {
                var overlappingShiftExists = await _dbContext.TimeShifts
                    .AnyAsync(p => p.Id != data.Id
                            && (p.Day == data.Day)
                                   && ((request.StartTime >= p.StartTime && request.StartTime < p.EndTime)
                                       || (request.EndTime >= p.StartTime && request.EndTime <= p.EndTime)
                                   || (request.StartTime <= p.StartTime && request.EndTime >= p.EndTime))
                        , cancellationToken: cancellationToken);
                if (overlappingShiftExists) throw new BadRequestException("TIME_SHIFT_ALREADY_EXISTS_OR_OVERLAPS");
            }
        }
        var @event = new TimeShiftUpdatedEvent(_client.GetIdentifier(), data.Id, data.Sequence + 1, new TimeShiftUpdatedEventData()
        {
            PriceForFirstHour = request.PriceForFirstHour!.Value,
            PriceForRemainingHours = request.PriceForRemainingHours!.Value,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
        });
        data.Apply(@event);
        _dbContext.Entry(data).State = EntityState.Modified;
        await _dbContext.Events.AddAsync(@event, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new MessageResponse()
        {
            Msg = "TIME_SHIFT_UPDATED",
        };
    }
}
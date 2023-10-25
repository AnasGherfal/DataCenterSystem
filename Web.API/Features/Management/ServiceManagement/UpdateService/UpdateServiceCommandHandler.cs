using Core.Constants;
using Core.Events.Service;
using Core.Exceptions;
using Core.Interfaces.Services;
using Core.Wrappers;
using Infrastructure;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Features.ServiceManagement.UpdateService;

public sealed record UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand, MessageResponse>
{
    private readonly IClientService _client;
    private readonly AppDbContext _dbContext;

    public UpdateServiceCommandHandler(AppDbContext dbContext, IClientService client)
    {
        _dbContext = dbContext;
        _client = client;
    }

    public async Task<MessageResponse> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.Parse(request.Id!);
        var data = await _dbContext.Services
                       .SingleOrDefaultAsync(p => p.Id == id, cancellationToken: cancellationToken);
        if (data == null) throw new NotFoundException("!عذرًا لا وجود لباقة بهذا الرقم");
        if (data.Status == GeneralStatus.Locked) throw new BadRequestException($"! {data.Id}: عذرًا هذه الخدمة مقيدة");
        if (data.Name != request.Name)
        {
            var isNotUnique = await _dbContext.Services
                .Where(p => p.Name == request.Name)
                .AnyAsync(cancellationToken: cancellationToken);
            if (isNotUnique) throw new BadRequestException("!عذرًا ولكن هذا الأسم محجوز لباقة أخرى");
        }
        var @event = new ServiceUpdatedEvent(_client.GetIdentifier(), data.Id, data.Sequence + 1, new ServiceUpdatedEventData()
        {
            Name = request.Name!,
            AmountOfPower = request.AmountOfPower!,
            AcpPort = request.AcpPort!,
            Dns = request.Dns!,
            MonthlyVisits = request.MonthlyVisits!.Value,
            Price = request.Price!.Value,
        });
        data.Apply(@event);
        _dbContext.Entry(data).State = EntityState.Modified;
        await _dbContext.Events.AddAsync(@event, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new MessageResponse()
        {
            Msg = "تمت إضافة الباقة بنجاح",
        };
    }
}
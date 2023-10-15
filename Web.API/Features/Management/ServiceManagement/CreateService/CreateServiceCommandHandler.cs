using Core.Entities;
using Core.Events.Service;
using Core.Exceptions;
using Core.Interfaces.Services;
using Core.Wrappers;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Features.ServiceManagement.CreateService;

public sealed record CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, MessageResponse>
{
    private readonly IClientService _client;
    private readonly AppDbContext _dbContext;

    public CreateServiceCommandHandler(AppDbContext dbContext, IClientService client)
    {
        _dbContext = dbContext;
        _client = client;
    }

    public async Task<MessageResponse> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
    {
        var dataExists = await _dbContext.Services.AnyAsync(p => p.Name == request.Name, cancellationToken: cancellationToken);
        if (dataExists) throw new BadRequestException("عذرًا ولكن هذا الأسم موجود مسبقًا");
        var @event = new ServiceCreatedEvent(_client.GetIdentifier(), Guid.NewGuid(), new ServiceCreatedEventData()
        {
            Name = request.Name!,
            AmountOfPower = request.AmountOfPower!,
            AcpPort = request.AcpPort!,
            Dns = request.Dns!,
            MonthlyVisits = request.MonthlyVisits!.Value,
            Price = request.Price!.Value,
        });
        var data = new Service();
        data.Apply(@event);
        await _dbContext.Services.AddAsync(data, cancellationToken);
        await _dbContext.Events.AddAsync(@event, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new MessageResponse()
        {
            Msg = "تمت إضافة الباقة بنجاح",
        };
    }
}
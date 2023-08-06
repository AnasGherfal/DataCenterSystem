using Infrastructure;
using Infrastructure.Audits.Service;
using Infrastructure.Constants;
using Infrastructure.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Exceptions;

namespace Web.API.Features.ServiceManagement.CreateService;

public sealed record CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, MessageResponse>
{
    private readonly DataCenterContext _dbContext;

    public CreateServiceCommandHandler(DataCenterContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<MessageResponse> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
    {
        var dataExists = await _dbContext.Services.AnyAsync(p => p.Name == request.Name && p.Status != GeneralStatus.Deleted, cancellationToken: cancellationToken);
        if (dataExists) throw new BadRequestException("عذرًا ولكن هذا الأسم موجود مسبقًا");
        var @event = new ServiceCreatedAudit("", Guid.NewGuid(), new ServiceCreatedAuditData()
        {
            Name = request.Name!,
            AmountOfPower = request.AmountOfPower!,
            AcpPort = request.AcpPort!,
            Dns = request.Dns!,
            MonthlyVisits = request.MonthlyVisits!.Value,
            Price = request.Price!.Value,
            Photo = "",
        });
        await _dbContext.Services.AddAsync(Service.Create(@event), cancellationToken);
        await _dbContext.Audits.AddAsync(@event, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new MessageResponse()
        {
            Msg = "تمت إضافة الباقة بنجاح",
        };
    }
}
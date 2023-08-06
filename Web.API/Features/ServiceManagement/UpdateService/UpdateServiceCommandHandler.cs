using Common.Constants;
using Infrastructure;
using Infrastructure.Audits.Admin;
using Infrastructure.Audits.Service;
using Infrastructure.Constants;
using Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Exceptions;

namespace Web.API.Features.ServiceManagement.UpdateService;

public sealed record UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand, MessageResponse>
{
    private readonly DataCenterContext _dbContext;

    public UpdateServiceCommandHandler(DataCenterContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<MessageResponse> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.Parse(request.Id!);
        var data = await _dbContext.Services
                       .SingleOrDefaultAsync(p => p.Id == id && p.Status != GeneralStatus.Deleted, cancellationToken: cancellationToken);
        if (data == null) throw new NotFoundException("!عذرًا لا وجود لباقة بهذا الرقم");
        if (IsLocked(data.Status)) throw new BadRequestException($"! {data.Id}: عذرًا هذه الباقة مقيدة");
        if (data.Name != request.Name)
        {
            var isNotUnique = await _dbContext.Services
                .Where(p => p.Name == request.Name && p.Status != GeneralStatus.Deleted)
                .AnyAsync(cancellationToken: cancellationToken);
            if (isNotUnique) throw new BadRequestException("!عذرًا ولكن هذا الأسم محجوز لباقة أخرى");
        }
        var @event = new ServiceUpdatedAudit("", Guid.NewGuid(), new ServiceUpdatedAuditData()
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
        await _dbContext.Audits.AddAsync(@event, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new MessageResponse()
        {
            Msg = "تمت إضافة الباقة بنجاح",
        };
    }
    
    private static bool IsLocked(GeneralStatus status)
    {
        return status switch
        {
            GeneralStatus.Active => false,
            GeneralStatus.Locked => true,
            _ => true,
        };
    }
}
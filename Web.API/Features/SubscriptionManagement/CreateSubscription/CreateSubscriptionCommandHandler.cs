using Infrastructure;
using Infrastructure.Audits.Subscription;
using Infrastructure.Constants;
using Infrastructure.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Exceptions;
using Web.API.Services.UploadService;

namespace Web.API.Features.SubscriptionManagement.CreateSubscription;

public sealed record CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, MessageResponse>
{
    private readonly IUploadFileService _uploadFile;
    private readonly DataCenterContext _dbContext;

    public CreateSubscriptionCommandHandler(DataCenterContext dbContext, IUploadFileService uploadFile)
    {
        _dbContext = dbContext;
        _uploadFile = uploadFile;
    }

    public async Task<MessageResponse> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var serviceId = Guid.Parse(request.ServiceId!);
        var serviceExists = await _dbContext.Services.AnyAsync(p => p.Id == serviceId 
            && p.Status == GeneralStatus.Active, cancellationToken: cancellationToken);
        if (!serviceExists) throw new BadRequestException("الخدمة غير موجودة");
        var customerId = Guid.Parse(request.CustomerId!);
        var customerExists = await _dbContext.Customers.AnyAsync(p => p.Id == customerId 
            && p.Status == GeneralStatus.Active, cancellationToken: cancellationToken);
        if (!customerExists) throw new BadRequestException("العميل غير موجود");
        var uploadPath = await _uploadFile.Upload(request.File!, StorageType.SubscriptionFile);
        var @event = new SubscriptionCreatedAudit("", Guid.NewGuid(), new SubscriptionCreatedAuditData()
        {
            ServiceId = serviceId,
            CustomerId = customerId,
            StartDate = DateTime.Parse(request.StartDate!),
            EndDate = DateTime.Parse(request.EndDate!),
            FileIdentifier = Guid.NewGuid(),
            FileType = request.DocType!.Value,
            File = uploadPath,
        });
        await _dbContext.Subscriptions.AddAsync(Subscription.Create(@event), cancellationToken);
        await _dbContext.Audits.AddAsync(@event, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new MessageResponse()
        {
            Msg = "تم اضافة الإشتراك بنجاح!",
        };
    }
}
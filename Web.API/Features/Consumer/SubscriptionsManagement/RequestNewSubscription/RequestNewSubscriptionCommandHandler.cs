using Core.Constants;
using Core.Entities;
using Core.Events.Subscription;
using Core.Exceptions;
using Core.Interfaces.Dtos;
using Core.Interfaces.Services;
using Core.Wrappers;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Features.Consumer.SubscriptionsManagement.RequestNewSubscription;

public sealed record RequestNewSubscriptionCommandHandler : IRequestHandler<RequestNewSubscriptionCommand, MessageResponse>
{
    private readonly IClientService _client;
    private readonly IUploadFileService _uploadFile;
    private readonly AppDbContext _dbContext;
    private readonly UserManager<Account> _customerManager;

    public RequestNewSubscriptionCommandHandler(AppDbContext dbContext, IUploadFileService uploadFile, IClientService client, UserManager<Account> customerManager)
    {
        _dbContext = dbContext;
        _uploadFile = uploadFile;
        _client = client;
        _customerManager = customerManager;
    }

    public async Task<MessageResponse> Handle(RequestNewSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var serviceId = Guid.Parse(request.ServiceId!);
        var serviceExists = await _dbContext.Services.AnyAsync(p => p.Id == serviceId 
            && p.Status == GeneralStatus.Active, cancellationToken: cancellationToken);
        if (!serviceExists) throw new BadRequestException("الخدمة غير موجودة");
        var customerId = _client.GetIdentifier();
        var customerExists = await _dbContext.Customers.AnyAsync(p => p.Id == customerId 
            && p.Status == GeneralStatus.Active, cancellationToken: cancellationToken);
        if (!customerExists) throw new BadRequestException("العميل غير موجود");
        var uploadPath = await _uploadFile.UploadFiles(StorageType.SubscriptionFile, new List<FileStorageUploadRequest>()
        {
            new(Guid.NewGuid(), request.File!,(short)DocumentType.SubscriptionFile)
        });
        if (uploadPath == null) throw new BadRequestException("حدث خطأ أثناء رفع الملف");
        var @event = new SubscriptionRequestedEvent(_client.GetIdentifier(), Guid.NewGuid(), new SubscriptionRequestedEventData()
        {
            ServiceId = serviceId,
            CustomerId = customerId,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddYears(1),
            Documents = new() 
            {
                FileIdentifier = uploadPath.First().Id,
                FileType = DocumentType.SubscriptionFile,
                FileLink = uploadPath.First().Link,
            },
        });
        var data = new Subscription();
        data.Apply(@event);
        await _dbContext.Subscriptions.AddAsync(data, cancellationToken);
        await _dbContext.Events.AddAsync(@event, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new MessageResponse()
        {
            Msg = "تم طلب الإشتراك بنجاح!",
        };
    }
}
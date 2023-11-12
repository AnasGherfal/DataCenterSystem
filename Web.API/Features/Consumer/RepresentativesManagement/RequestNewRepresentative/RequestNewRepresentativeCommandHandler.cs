using Core.Constants;
using Core.Entities;
using Core.Events.Abstracts;
using Core.Events.Representative;
using Core.Exceptions;
using Core.Interfaces.Dtos;
using Core.Interfaces.Services;
using Core.Wrappers;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Features.Consumer.RepresentativesManagement.RequestNewRepresentative;

public sealed record RequestNewRepresentativeCommandHandler : IRequestHandler<RequestNewRepresentativeCommand, MessageResponse>
{
    private readonly IClientService _client;
    private readonly IUploadFileService _uploadFile;
    private readonly AppDbContext _dbContext;
    private readonly UserManager<Account> _customerManager;

    public RequestNewRepresentativeCommandHandler(AppDbContext dbContext, IUploadFileService uploadFile, IClientService client, UserManager<Account> customerManager)
    {
        _dbContext = dbContext;
        _uploadFile = uploadFile;
        _client = client;
        _customerManager = customerManager;
    }

    public async Task<MessageResponse> Handle(RequestNewRepresentativeCommand request, CancellationToken cancellationToken)
    {
        var customerId = _client.GetIdentifier();
        var customerExists = await _customerManager.Users
            .AnyAsync(p => p.Id == customerId, cancellationToken: cancellationToken);
        if (!customerExists) throw new BadRequestException("العميل غير موجود");
        var countRepresentatives = await _dbContext.Representatives
            .CountAsync(p => p.CustomerId == customerId, cancellationToken: cancellationToken);
        if (countRepresentatives >= 25) throw new BadRequestException("العميل لديه الحد الأقصى من المخولين");
        var fileRequests = new List<FileStorageUploadRequest>()
        {
            new(Guid.NewGuid(), request.IdentityDocument!, (short) DocumentType.IdentityDocument),
            new(Guid.NewGuid(), request.RepresentationDocument!, (short) DocumentType.CompanyDocument)
        };
        var uploadPath = await _uploadFile.UploadFiles(StorageType.RepresentativeFile, fileRequests);
        if (uploadPath == null) throw new BadRequestException("حدث خطأ أثناء رفع الملف");
        var @event = new RepresentativeRequestedEvent(_client.GetIdentifier(), Guid.NewGuid(), new RepresentativeRequestedEventData()
        {
            CustomerId = customerId,
            RepresentativeType = request.Type!.Value,
            ActiveFrom = string.IsNullOrEmpty(request.From) ? null : DateTime.Parse(request.From!),
            ActiveTo = string.IsNullOrEmpty(request.To) ? null : DateTime.Parse(request.To!),
            FirstName = request.FirstName!,
            LastName = request.LastName!,
            IdentityNo = request.IdentityNo!,
            IdentityType = request.IdentityType!.Value,
            Email = request.Email!,
            PhoneNo = request.PhoneNo!,
            Documents = fileRequests.Select(p => new FileStorageData()
            {
                FileIdentifier = p.Id,
                FileType = (DocumentType) p.FileType,
                FileLink = uploadPath.First(q => q.Id == p.Id).Link,
            }).ToList()
        });
        var data = new Representative();
        data.Apply(@event);
        await _dbContext.Representatives.AddAsync(data, cancellationToken);
        await _dbContext.Events.AddAsync(@event, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new MessageResponse()
        {
            Msg = "تم اضافة الإشتراك بنجاح!",
        };
    }
}
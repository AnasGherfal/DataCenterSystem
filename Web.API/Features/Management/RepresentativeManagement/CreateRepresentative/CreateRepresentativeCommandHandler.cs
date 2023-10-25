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

namespace Web.API.Features.RepresentativeManagement.CreateRepresentative;

public sealed record CreateRepresentativeCommandHandler : IRequestHandler<CreateRepresentativeCommand, MessageResponse>
{
    private readonly IClientService _client;
    private readonly IUploadFileService _uploadFile;
    private readonly AppDbContext _dbContext;
    private readonly UserManager<Customer> _customerManager;

    public CreateRepresentativeCommandHandler(AppDbContext dbContext, IUploadFileService uploadFile, IClientService client, UserManager<Customer> customerManager)
    {
        _dbContext = dbContext;
        _uploadFile = uploadFile;
        _client = client;
        _customerManager = customerManager;
    }

    public async Task<MessageResponse> Handle(CreateRepresentativeCommand request, CancellationToken cancellationToken)
    {
        var customerId = Guid.Parse(request.CustomerId!);
        var customerExists = await _customerManager.Users
            .AnyAsync(p => p.Id == customerId, cancellationToken: cancellationToken);
        if (!customerExists) throw new BadRequestException("العميل غير موجود");
        var countRepresentatives = await _dbContext.Representatives
            .CountAsync(p => p.CustomerId == customerId, cancellationToken: cancellationToken);
        if (countRepresentatives >= 2) throw new BadRequestException("العميل لديه الحد الأقصى من المخولين");
        var fileRequests = new List<FileStorageUploadRequest>()
        {
            new(Guid.NewGuid(), request.IdentityDocument!, (short) DocumentType.IdentityDocument),
            new(Guid.NewGuid(), request.RepresentationDocument!, (short) DocumentType.CompanyDocument)
        };
        var uploadPath = await _uploadFile.UploadFiles(StorageType.RepresentativeFile, fileRequests);
        if (uploadPath == null) throw new BadRequestException("حدث خطأ أثناء رفع الملف");
        var @event = new RepresentativeCreatedEvent(_client.GetIdentifier(), Guid.NewGuid(), new RepresentativeCreatedEventData()
        {
            CustomerId = customerId,
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
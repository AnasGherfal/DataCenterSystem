using Infrastructure;
using Infrastructure.Constants;
using Infrastructure.Entities;
using Infrastructure.Events.Abstracts;
using Infrastructure.Events.Customer;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Exceptions;
using Web.API.Services.ClientService;
using Web.API.Services.UploadService;
using Web.API.Services.UploadService.Dtos;

namespace Web.API.Features.CustomerManagement.CreateCustomer;

public sealed record CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, MessageResponse>
{
    private readonly IClientService _client;
    private readonly IUploadFileService _uploadFile;
    private readonly AppDbContext _dbContext;

    public CreateCustomerCommandHandler(AppDbContext dbContext, IUploadFileService uploadFile, IClientService client)
    {
        _dbContext = dbContext;
        _uploadFile = uploadFile;
        _client = client;
    }

    public async Task<MessageResponse> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customerExists = await _dbContext.Customers
            .AnyAsync(p => p.PrimaryPhone == request.PrimaryPhone!
                || p.Email == request.Email!, cancellationToken: cancellationToken);
        if (customerExists) throw new BadRequestException("العميل موجود بالفعل");
        var fileRequests = request.Documents!
            .Select(p => new FileStorageUploadRequest(Guid.NewGuid(), p.File!, (short) p.DocType!.Value))
            .ToList();
        var uploadPath = await _uploadFile.UploadFiles(StorageType.CustomerFile, fileRequests);
        if (uploadPath == null) throw new BadRequestException("حدث خطأ أثناء رفع الملف");
        var @event = new CustomerCreatedEvent(_client.GetIdentifier(), Guid.NewGuid(), new CustomerCreatedEventData()
        {
            Name = request.Name!,
            Address = request.Address!,
            City = request.City!,
            PrimaryPhone = request.PrimaryPhone!,
            SecondaryPhone = request.SecondaryPhone ?? "",
            Email = request.Email!,
            Documents = fileRequests.Select(p => new FileStorageData()
            {
                FileIdentifier = p.Id,
                FileType = request.Documents!.First(q => q.File == p.File).DocType!.Value,
                FileLink = uploadPath.First(q => q.Id == p.Id).Link,
            }).ToList()
        });
        var data = new Customer();
        data.Apply(@event);
        await _dbContext.Customers.AddAsync(data, cancellationToken);
        await _dbContext.Events.AddAsync(@event, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new MessageResponse()
        {
            Msg = "تم اضافة مشترك بنجاح!",
        };
    }
}
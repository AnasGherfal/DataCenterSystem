using System.Security.Claims;
using Core.Constants;
using Core.Entities;
using Core.Events.Abstracts;
using Core.Events.Customer;
using Core.Exceptions;
using Core.Interfaces.Dtos;
using Core.Interfaces.Services;
using Core.Wrappers;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Features.CustomerManagement.CreateCustomer;

public sealed record CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, MessageResponse>
{
    private readonly IMediator _mediator;
    private readonly IClientService _client;
    private readonly IUploadFileService _uploadFile;
    private readonly AppDbContext _dbContext;
    private readonly UserManager<Account> _customerManager;

    public CreateCustomerCommandHandler(AppDbContext dbContext, IUploadFileService uploadFile, IClientService client, UserManager<Account> customerManager, IMediator mediator)
    {
        _uploadFile = uploadFile;
        _client = client;
        _dbContext = dbContext;
        _customerManager = customerManager;
        _mediator = mediator;
    }

    public async Task<MessageResponse> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customerExists = await _dbContext.Customers
            .AnyAsync(p => p.PrimaryPhone == request.PrimaryPhone!
                || p.Email == request.Email!, cancellationToken: cancellationToken);
        if (customerExists) throw new BadRequestException("العميل موجود بالفعل");
        var fileRequests = new List<FileStorageUploadRequest>()
        {
            new(Guid.NewGuid(), request.IdentityDocument!, (short) DocumentType.IdentityDocument),
            new(Guid.NewGuid(), request.CompanyDocuments!, (short) DocumentType.CompanyDocument)
        };
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
                FileType = (DocumentType) p.FileType,
                FileLink = uploadPath.First(q => q.Id == p.Id).Link,
            }).ToList()
        });
        var data = new Account();
        data.Apply(@event);
        var password = GeneratePassword();
        var result = await _customerManager.CreateAsync(data, password);
        if (!result.Succeeded) throw new BadRequestException(result.Errors.FirstOrDefault()!.Description);
        await _customerManager.AddClaimsAsync(data, new List<Claim>()
        {
            new(ClaimsKey.IdentityId.Key(), data.Id.ToString()),
            new(ClaimsKey.DisplayName.Key(), data.Customer.Name),
            new(ClaimsKey.Email.Key(), data.Email ?? ""),
            new(ClaimsKey.EmailVerified.Key(), false.ToString()),
            new(ClaimsKey.UserType.Key(), AccountType.Customer.ToString("D")),
        });
        await _dbContext.Events.AddAsync(@event, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        // await _mediator.Publish(new CreateCustomerCommandNotification()
        // {
        //     Content = request,
        //     Password = password,
        // }, cancellationToken);
        return new MessageResponse()
        {
            Msg = "تم اضافة مشترك بنجاح!",
        };
    }
    
    private static readonly Random Random = new();
    private string GeneratePassword()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var newPassword = (Enumerable.Repeat(chars, 8).Select(s => s[Random.Next(s.Length)]).ToArray());
        return  new string(newPassword);
    }
}
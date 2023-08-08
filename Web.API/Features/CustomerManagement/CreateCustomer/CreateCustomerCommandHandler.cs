using AutoMapper;
using Infrastructure;
using Infrastructure.Constants;
using Infrastructure.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Constants;
using Shared.Dtos;
using Shared.Exceptions;

namespace Web.API.Features.CustomerManagement.CreateCustomer;

public sealed record CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, MessageResponse>
{
    private readonly DataCenterContext _dbContext;
    private readonly IConfiguration _config;
    public CreateCustomerCommandHandler(DataCenterContext dbContext, IConfiguration config)
    {
        _dbContext = dbContext;
        _config = config;
        
    }
    public async Task<MessageResponse> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var data = new Customer
        {
            Name = request.Name!,
            Address = request.Address,
            PrimaryPhone = request.PrimaryPhone!,
            SecondaryPhone = request.SecondaryPhone,
            Email = request.Email!,
            Status = GeneralStatus.Active,
            CreatedOn = DateTime.UtcNow,       
        } ?? throw new BadRequestException("! طلبك غير صالح يرجى إعادة المحاولة");
        var isNotUnique = await _dbContext.Customers
            .Where(p => p.Name == request.Name && p.Status != GeneralStatus.Deleted)
            .AnyAsync();
        if (isNotUnique) throw new NotFoundException("الاسم موجود مسبقًا");
        await _dbContext.Customers.AddAsync(data);
        var customerFiles = new List<CustomerFile>()
        {
            new CustomerFile()
            {
                DocType=request.FirstFile.DocType,
                Filename=ToTrustedFileName(data.Name,(DocType)request.FirstFile.DocType,Path.GetExtension(request.FirstFile.File.FileName)),
                FilePath=GetFilePath(data.Name),
                CustomerId=data.Id,
                Customer=data,
                 IsActive=GeneralStatus.Active,
                CreatedOn=DateTime.UtcNow
            },
              new CustomerFile()
            {
                DocType=request.SecondFile.DocType,
                Filename=ToTrustedFileName(data.Name,(DocType)request.SecondFile.DocType,Path.GetExtension(request.SecondFile.File.FileName)),
                FilePath=GetFilePath(data.Name),
                CustomerId=data.Id,
                Customer=data,
                IsActive=GeneralStatus.Active,
                CreatedOn=DateTime.UtcNow
            }
        };
        foreach (var file in customerFiles)
        {
            _dbContext.CustomerFiles.Add(file);
        }
        await _dbContext.SaveChangesAsync();
    
       


        return new MessageResponse()
        {
            Msg = "تمت إضافة العميل بنجاح!",
        };
    }

    private string GetFilePath(string name)
    {
        return _config.GetValue<string>("Storage:Customer") + "\\" + DateOnly.FromDateTime(DateTime.UtcNow).Year.ToString() + "\\" + $"{name}\\";
    }
    private static string ToTrustedFileName(string name,DocType type,string ext)
    {
        return name + $" {type}" + ext;
    }
}


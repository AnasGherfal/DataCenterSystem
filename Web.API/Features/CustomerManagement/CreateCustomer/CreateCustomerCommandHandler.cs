using AutoMapper;
using Infrastructure;
using Infrastructure.Constants;
using Infrastructure.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
        await _dbContext.SaveChangesAsync();
    
       


        return new MessageResponse()
        {
            Msg = "تمت إضافة العميل بنجاح!",
        };
    }

    private string GetFilePath(string customerName)
    {
        return _config.GetValue<string>("Storage:Customer") + "\\" + DateOnly.FromDateTime(DateTime.UtcNow).Year.ToString() + "\\" + $"{customerName}\\";

    }
    private static string ToTrustedFileName(string ext)
    {
        return Guid.NewGuid().ToString() + ext;
    }
}


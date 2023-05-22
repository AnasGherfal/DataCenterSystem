using AutoMapper;
using Infrastructure;
using Infrastructure.Models;
using ManagementAPI.Dtos.Customer;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using System.Net;
using AutoMapper.QueryableExtensions;
using Infrastructure.Constants;
using Shared.Exceptions;
using Shared.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ManagementAPI.Services;

public class CustomerService : ICustomerService
{
    private readonly DataCenterContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IConfiguration _config;

    public CustomerService(DataCenterContext dbContext, IMapper mapper, IConfiguration config)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _config = config;
    }

    public async Task<MessageResponse> Create(CreateCustomerRequestDto request)
    {
        var data = _mapper.Map<Customer>(request);
        if (data == null) throw new BadRequestException("! طلبك غير صالح يرجى إعادة المحاولة");
        var isNotUnique = await _dbContext.Customers
            .Where(p => p.Name == request.Name && p.Status != GeneralStatus.Deleted)
            .AnyAsync();
        if (isNotUnique) throw new NotFoundException("الاسم موجود مسبقًا");
        await _dbContext.Customers.AddAsync(data);
        await _dbContext.SaveChangesAsync();
        int counter = 0;
       foreach (var item in request.Files)
       {
            var file = item.File;
            var path = GetFilePath(request.Name);
            var ext = Path.GetExtension(file.FileName);
            var fullFileName = ToTrustedFileName(ext);

            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            string fullPath = Path.Combine(path, fullFileName);
            if (System.IO.File.Exists(fullPath))
            {

            }
            using (FileStream stream = System.IO.File.Create(fullPath))
            {
                await file.CopyToAsync(stream);
            var customerFile =_mapper.Map<CustomerFile>(file);
            customerFile.Filename = fullPath;
            customerFile.DocType = item.DocType;
            customerFile.Customer = data;
            _dbContext.CustomerFiles.Add(customerFile);
            await _dbContext.SaveChangesAsync();
            }
            counter++;
        }
             
        
        return new MessageResponse()
        {
            Msg = "تمت إضافة العميل بنجاح!",
        };
    }

    public async Task<CustomerResponseDto> GetById(int id)
    {
        if (id <= 0)
            throw new BadHttpRequestException("عذرًا رقم العميل الذي أدخلته غير صالح!");
        var data = _dbContext.Customers.Where(p => p.Id == id && p.Status != GeneralStatus.Deleted)
                                       .ProjectTo<CustomerResponseDto>(_mapper.ConfigurationProvider)
                                       .SingleOrDefault();
        if (data == null) throw new NotFoundException("عذرًا لا وجود لعميل بهذا الرقم يرجى التأكد!");
        return data;
    }
    public async Task<FetchCustomersResponseDto> GetAll(FetchCustomersRequestDto request)
    {
        var query = _dbContext.Customers
            .Include(p => p.Files)
            .Where(p => p.Status != GeneralStatus.Deleted);
        
        var files = query.Select(p => p.Files.Select(x => x.Filename)).ToList();
        
        var queryResult = await query.OrderBy(p => p.Id)
            .Skip(request.PageSize * (request.PageNumber - 1))
            .Take(request.PageSize)
            .ProjectTo<CustomerResponseDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        
        var totalCount = query.Count();
        var totalPages = Math.Ceiling(totalCount / (double)request.PageSize);
        return new FetchCustomersResponseDto(request.PageNumber, (int)totalPages, queryResult);
    }

    public async Task<MessageResponse> Update(int id, UpdateCustomerRequestDto request)
    {
        var data = await _dbContext.Customers
            .Where(p => p.Id == id && p.Status != GeneralStatus.Deleted)
            .Include(p => p.Files)
            .FirstOrDefaultAsync();
        if (data == null) throw new BadRequestException(" يرجى التأكد من صحة رقم العميل");
        if (data.Name != request.Name)
        {
            var isNotUnique = await _dbContext.Customers
                .Where(p => p.Name == request.Name && p.Status != GeneralStatus.Deleted)
                .AnyAsync();
            if (isNotUnique) throw new BadRequestException("الاسم موجود مسبقًا");
        }
        var oldFiles = data.Files.ToList();
        if (request.Files != null)
           foreach (var file in oldFiles)
                file.IsActive = 0;
              

        _mapper.Map(request, data);
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "! تم تعديل العميل بنجاح"
        };
    }

    public async Task<MessageResponse> Delete(int id)
    {
        var data = await _dbContext.Customers
            .Where(p => p.Id == id && p.Status == GeneralStatus.Active)
            .FirstOrDefaultAsync();
        if (data == null) throw new NotFoundException("عفوًا لا وجود لعميل بهذا الرقم");
        data.Status = GeneralStatus.Deleted;
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "! بنجاح " + data.Name + " : لقد تم حذف العميل",
        };
    }

    public async Task<MessageResponse> Lock(int id)
    {
        var data = await _dbContext.Customers
            .Where(p => p.Id == id && p.Status == GeneralStatus.Active)
            .FirstOrDefaultAsync();
        if (data == null) throw new NotFoundException("! عذرًا..لا وجود لعميل بهذا الرقم او هذا العميل مقيد مسبقًا");
        data.Status = GeneralStatus.LockedByUser;
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "! بنجاح " + data.Name + " : لقد تم تقييد العميل"
        };
    }

    public async Task<MessageResponse> Unlock(int id)
    {
        var data = await _dbContext.Customers
            .Where(p => p.Id == id && p.Status != GeneralStatus.LockedByUser)
            .FirstOrDefaultAsync();
        if (data == null) throw new NotFoundException("! عذرًا..لا وجود لعميل بهذا الرقم او هذا العميل غير مقيد");
        data.Status = GeneralStatus.Active;
        await _dbContext.SaveChangesAsync();
        return new OperationResponse()
        {
            Msg = "بنجاح " + data.Name + " :  تم إلغاء التقييد عن العميل",
            StatusCode = HttpStatusCode.OK
        };
    }
private string GetFilePath(string customerName)
{
    return _config.GetValue<string>("Storage:Customer")+"\\" +DateOnly.FromDateTime(DateTime.UtcNow).Year.ToString()+"\\" + $"\\{customerName}\\";
      
}
private string ToTrustedFileName(string ext)
{
    return Guid.NewGuid().ToString() + ext;
}

}


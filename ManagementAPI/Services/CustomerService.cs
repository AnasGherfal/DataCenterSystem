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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ManagementAPI.Services;

public class CustomerService : ICustomerService
{
    private readonly DataCenterContext _dbContext;
    private readonly IUploadFileService _uploadFile;
    private readonly IMapper _mapper;

    public CustomerService(DataCenterContext dbContext, IMapper mapper, IUploadFileService uploadFile)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _uploadFile = uploadFile;
    }

    public async Task<MessageResponse> Create(CreateCustomerRequestDto request)
    {
        var data = _mapper.Map<Customer>(request) ?? throw new BadRequestException("! طلبك غير صالح يرجى إعادة المحاولة");
        var isNotUnique = await _dbContext.Customers
            .Where(p => p.Name == request.Name && p.Status != GeneralStatus.Deleted)
            .AnyAsync();
        if (isNotUnique) throw new NotFoundException("الاسم موجود مسبقًا");
        await _dbContext.Customers.AddAsync(data);
        await _dbContext.SaveChangesAsync();
       
            
        await _uploadFile.Upload(request.FirstFile,EntityType.CustomerFile,data);
        await _uploadFile.Upload(request.SecondFile, EntityType.CustomerFile, data);
      
             
        
        return new MessageResponse()
        {
            Msg = "تمت إضافة العميل بنجاح!",
        };
    }

    public async Task<CustomerResponseDto> GetById(Guid id)
    {

        var data = await _dbContext.Customers.Where(p => p.Id == id && p.Status != GeneralStatus.Deleted)
                                       .Include(p => p.Representatives)
                                       .Include(p => p.Subscriptions)
                                       .Include(p => p.Files.Where(c=>c.IsActive!=GeneralStatus.Deleted))
                                       .ProjectTo<CustomerResponseDto>(_mapper.ConfigurationProvider)
                                       .SingleOrDefaultAsync() ?? throw new NotFoundException("عذرًا لا وجود لعميل بهذا الرقم يرجى التأكد!");
        return data ;
    }
    public async Task<FileStream> Download(Guid id)
    {
        var data = await _dbContext.CustomerFiles.SingleOrDefaultAsync(a => a.Id == id && a.IsActive == GeneralStatus.Active) ?? throw new BadRequestException("عذرًا لا وجود لملفات لهذا العميل..");
        var path = data.FilePath;
        // Check if the file exists.
        if (!File.Exists(path))
        {
            throw new FileNotFoundException("لم يتم العثور على الملف.. ");
        }

        // Open the file for reading.
        return File.OpenRead(path);
    }
    public async Task<FetchCustomersResponseDto> GetAll(FetchCustomersRequestDto request)
    {
       var query = _dbContext.Customers
            .Include(p=>p.Representatives.Where(c => c.Status != GeneralStatus.Deleted))
            .Include(p=>p.Subscriptions.Where(c => c.Status != GeneralStatus.Deleted))
            .Include(p => p.Files.Where(c=>c.IsActive!=GeneralStatus.Deleted))
            .Where(p => p.Status  != GeneralStatus.Deleted);
        if (request.CustomerName != null)
            query = _dbContext.Customers
                .Include(p => p.Representatives.Where(c => c.Status != GeneralStatus.Deleted))
                .Include(p => p.Subscriptions.Where(c => c.Status != GeneralStatus.Deleted))
                .Include(p => p.Files.Where(c => c.IsActive != GeneralStatus.Deleted))
                .Where(p => p.Status != GeneralStatus.Deleted && p.Name.Contains(request.CustomerName)); 
        var queryResult = await query.OrderBy(p => p.Id)
            .Skip(request.PageSize * (request.PageNumber - 1))
            .Take(request.PageSize)
            .ProjectTo<CustomerResponseDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        var totalCount = query.Count();
        var totalPages = Math.Ceiling(totalCount / (double)request.PageSize);
        return new FetchCustomersResponseDto(request.PageNumber, (int)totalPages,queryResult);
    }

    public async Task<MessageResponse> Update(Guid id, UpdateCustomerRequestDto request)
    {
        var data = await _dbContext.Customers
            .Where(p => p.Id == id && p.Status != GeneralStatus.Deleted)
            .Include(p => p.Files)
            .FirstOrDefaultAsync() ?? throw new BadRequestException(" يرجى التأكد من صحة رقم العميل");
        if (data.Name != request.Name)
        {
            var isNotUnique = await _dbContext.Customers
                .Where(p => p.Name == request.Name && p.Status != GeneralStatus.Deleted)
                .AnyAsync();
            if (isNotUnique) throw new BadRequestException("الاسم موجود مسبقًا");
        }
        var oldFiles = data.Files.ToList();
        if (oldFiles.Count==0)
        {
            await _uploadFile.Upload(request.FirstFile, EntityType.CustomerFile, data);
            await _uploadFile.Upload(request.SecondFile, EntityType.CustomerFile, data);
        }
        else
        {
            if (request.FirstFile != null)
                foreach (var file in oldFiles)
                {
                    if (file.DocType == request.FirstFile.DocType)
                    {
                        file.IsActive = GeneralStatus.Deleted;
                        await _uploadFile.Upload(request.FirstFile, EntityType.CustomerFile, data);
                    }

                }
            if (request.SecondFile != null)
                foreach (var file in oldFiles)
                {
                    if (file.DocType == request.SecondFile.DocType)
                    {
                        file.IsActive = GeneralStatus.Deleted;
                        await _uploadFile.Upload(request.SecondFile, EntityType.CustomerFile, data);
                    }

                }
        }
        _mapper.Map(request, data);
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "! تم تعديل العميل بنجاح"
        };
    }

    public async Task<MessageResponse> Delete(Guid id)
    {
        var data = await _dbContext.Customers
            .Include(p=> p.Files)
            .Where(p => p.Id == id && p.Status == GeneralStatus.Active)
            .FirstOrDefaultAsync() ?? throw new NotFoundException("عفوًا لا وجود لعميل بهذا الرقم");
        data.Status = GeneralStatus.Deleted;
        foreach(var file in data.Files)
            file.IsActive=GeneralStatus.Deleted;
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "! بنجاح " + data.Name + " : لقد تم حذف العميل",
        };
    }

    public async Task<MessageResponse> Lock(Guid id)
    {
        var data = await _dbContext.Customers
            .Include(p => p.Representatives)
            .Include(p => p.Files)
            .Where(p => p.Id == id && p.Status == GeneralStatus.Active)
            .FirstOrDefaultAsync() ?? throw new NotFoundException("! عذرًا..لا وجود لعميل بهذا الرقم او هذا العميل مقيد مسبقًا");
        data.Status = GeneralStatus.Locked;
        if(data.Representatives != null)
        foreach(var Representative in data.Representatives)
            Representative.Status = GeneralStatus.Locked;
        foreach (var file in data.Files)
            file.IsActive = GeneralStatus.Locked;
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "! بنجاح " + data.Name + " : لقد تم تقييد العميل"
        };
    }

    public async Task<MessageResponse> Unlock(Guid id)
    {
        var data = await _dbContext.Customers
            .Include(p => p.Representatives)
            .Include(p => p.Files)
            .Where(p => p.Id == id && p.Status == GeneralStatus.Locked)
            .FirstOrDefaultAsync() ?? throw new NotFoundException("! عذرًا..لا وجود لعميل بهذا الرقم او هذا العميل غير مقيد");
        data.Status = GeneralStatus.Active;
        if(data.Representatives != null)
        foreach(var Representative in data.Representatives)
        Representative.Status= GeneralStatus.Active;
        foreach (var file in data.Files)
            file.IsActive = GeneralStatus.Active;
        await _dbContext.SaveChangesAsync();
        return new OperationResponse()
        {
            Msg = "بنجاح " + data.Name + " :  تم إلغاء التقييد عن العميل",
            StatusCode = HttpStatusCode.OK
        };
    }


}


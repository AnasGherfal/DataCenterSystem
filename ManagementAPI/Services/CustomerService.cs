using AutoMapper;
using Infrastructure;
using Infrastructure.Models;
using ManagementAPI.Dtos.Customer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Common.Constants;
using Shared.Dtos;
using System.Net;
using System.Reflection.Metadata;
using Azure.Core;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Newtonsoft.Json.Linq;
using Shared.Constants;

namespace ManagementAPI.Services;

public class CustomerService:ICustomerService
{
    private readonly DataCenterContext _dbContext;
    private readonly IMapper _mapper;
    public CustomerService(DataCenterContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<OperationResponse> Create(CreateCustomerRequestDto request)
    {
        var data = _mapper.Map<Customer>(request);
        if (data == null)
            return new OperationResponse()
            { Msg = "! طلبك غير صالح يرجى إعادة المحاولة", StatusCode = HttpStatusCode.BadRequest };
        var isNotUnique = await _dbContext.Customers
                            .Where(p => p.Name == request.Name && p.Status != GeneralStatus.Deleted)
                            .AnyAsync();
        if(isNotUnique)
        return new OperationResponse()
        { 
            Msg = "الاسم موجود مسبقًا",
            StatusCode = HttpStatusCode.BadRequest 
        };
        await _dbContext.Customers.AddAsync(data);
        await _dbContext.SaveChangesAsync();
        return new OperationResponse() 
        { 
            Msg = "تمت إضافة العميل بنجاح!",
            StatusCode = HttpStatusCode.OK
        };
    }
    public async Task<FetchCustomersResponseDto> GetAll(FetchCustomersRequestDto request)
    {
        var query =_dbContext.Customers
                       .ProjectTo<CustomerResponseDto>(_mapper.ConfigurationProvider)
                        .Where(p => p.Status != GeneralStatus.Deleted);
        var queryResult = await query.OrderBy(p => p.Id)
                              .Skip(request.PageSize * (request.PageNumber - 1))
                              .Take(request.PageSize)
                              .ToListAsync();
        var totalCount = query.Count(); 
        var totalpages = Math.Ceiling(totalCount / (double)request.PageSize);
        return new FetchCustomersResponseDto() 
        {
            Content = queryResult,
            CurrentPage = request.PageNumber,
            TotalPages = (int)totalpages 
        };
    }
    public async Task<OperationResponse> Update(int id, UpdateCustomerRequestDto request)
    {
        if (id < 1)
            return new OperationResponse()
            {
                Msg = "! يرجى ادخال رقم عميل صحيح",
                StatusCode = HttpStatusCode.BadRequest
            };
        var data = await _dbContext.Customers
                         .Where(p => p.Id == id && p.Status != GeneralStatus.Deleted)
                         .FirstOrDefaultAsync();
        if (data == null)
            return new OperationResponse()
            {
                Msg = " يرجى التأكد من صحة رقم العميل",
                StatusCode = HttpStatusCode.BadRequest
            };
        if (data.Name != request.Name)
        {
            var isNotUnique = await _dbContext.Customers
                                    .Where(p => p.Name == request.Name && p.Status != GeneralStatus.Deleted)
                                    .AnyAsync();
            if (isNotUnique)
                return new OperationResponse()
                {
                    Msg = "الاسم موجود مسبقًا",
                    StatusCode = HttpStatusCode.BadRequest
                };
        }
        _mapper.Map(request, data);
        await _dbContext.SaveChangesAsync();
        return new OperationResponse()
        {
            Msg = "! تم تعديل العميل بنجاح",
            StatusCode = HttpStatusCode.OK
        };
    }
    public async Task<OperationResponse> Delete(int id)
    {
        if (id < 1)
            return new OperationResponse()
            {
                Msg = " يرجى ادخال رقم عميل صحيح",
                StatusCode = HttpStatusCode.BadRequest
            };
        var data = await _dbContext.Customers
                       .Where(p => p.Id == id && p.Status == GeneralStatus.Active)
                       .FirstOrDefaultAsync();
        if (data == null)
            return new OperationResponse()
            {
                Msg = "عفوًا لا وجود لعميل بهذا الرقم",
                StatusCode = HttpStatusCode.NotFound
            };
        data.Status = GeneralStatus.Deleted;
        await _dbContext.SaveChangesAsync();
        return new OperationResponse()
        {
            Msg = "! بنجاح " + data.Name + " : لقد تم حذف العميل",
            StatusCode = HttpStatusCode.OK
        };
    }
    public async Task<OperationResponse> Lock(int id)
    {
        if (id is <= 0)
            return new OperationResponse()
            {
                Msg = "الرجاء ادخال رقم عميل صحيح وموجود فعلًا",
                StatusCode = HttpStatusCode.BadRequest
            };

        var data = await _dbContext.Customers.Where(p => p.Id == id && p.Status == GeneralStatus.Active)
                                                 .FirstOrDefaultAsync();
        if(data == null)
            return new OperationResponse()
            {
                Msg = "! عذرًا..لا وجود لعميل بهذا الرقم",
                StatusCode = HttpStatusCode.BadRequest
            };

        if (!IsLocked(data.Status))
        {
            data.Status =GeneralStatus.LockedByUser;
            await _dbContext.SaveChangesAsync();

            return new OperationResponse()
            {
                Msg = "! بنجاح " + data.Name + " : لقد تم تقييد العميل",
                StatusCode = HttpStatusCode.OK
            };
        }
        else
            return new OperationResponse()
            {
                Msg = " ! عذرًا .. هذا العميل مقيد مسبقًا",
                StatusCode = HttpStatusCode.BadRequest
            };
    }

    public async Task<OperationResponse> Unlock(int id)
    {

        if (id <= 0)
            return new OperationResponse()
            {
                Msg = "الرجاء ادخال رقم عميل صحيح وموجود فعلًا",
                StatusCode = HttpStatusCode.BadRequest
            };

        var data = await _dbContext.Customers
                         .Where(p => p.Id == id && p.Status != GeneralStatus.Deleted)
                         .FirstOrDefaultAsync();
        if (data == null)
            return new OperationResponse()
            {
                Msg = "! عذرًا..لا وجود لعميل بهذا الرقم",
                StatusCode = HttpStatusCode.BadRequest
            };

        if (IsLocked(data.Status))
        {
            data.Status = GeneralStatus.Active;
            await _dbContext.SaveChangesAsync();
            return new OperationResponse()
            {
                Msg = "بنجاح " + data.Name + " :  تم إلغاء التقييد عن العميل",
                StatusCode = HttpStatusCode.OK
            };
        }
        else
        {
            return new OperationResponse()
            {
                Msg = " ! عذرًا .. هذا العميل غير مقيد",
                StatusCode = HttpStatusCode.BadRequest
            };
        }
    }
    private bool IsLocked(GeneralStatus status) 
    {
        switch (status)
        {
            case GeneralStatus.Active:
                return false;
            case GeneralStatus.LockedByUser:
                return true;
            default:
                return false;   
        }
    }
}


﻿using AutoMapper;
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
using Infrastructure.Constants;

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
    
    public async Task<OperationResponse> CreateCustomer(CreateCustomerRequestDto request)
    {
        if (await _dbContext.Customers.AnyAsync(p => p.Name.ToLower() == request.Name.ToLower()))
        {
            return new OperationResponse()
            {
                Msg = "thier is simellar name stored in database",
                StatusCode = HttpStatusCode.BadRequest,
            };
        }

        var NewCustomer = _mapper.Map<Customer>(request);
        NewCustomer.Status = GeneralStatus.Active;
        NewCustomer.CreatedOn = DateTime.UtcNow;
        NewCustomer.CreatedById = 3;
        if (NewCustomer == null)
            return new OperationResponse()
            { Msg = "طلبك غير صالح يرجى إدخال بيانات العميل", StatusCode = HttpStatusCode.BadRequest };

        await _dbContext.Customers.AddAsync(NewCustomer);
        await _dbContext.SaveChangesAsync();
        return new OperationResponse() { Msg = "ok", StatusCode = HttpStatusCode.OK };
    }


    public async Task<FetchCustomersResponseDto> GetAllCustomer(int pgSize, int pgNum)
    {

        var CustQuery = await (from Cust in _dbContext.Customers
                               .OrderBy(x => x.Id) where Cust.Status != GeneralStatus.Deleted
                                select Cust)
                               .Skip(pgSize * (pgNum - 1))
                               .Take(pgSize)
                               .ToListAsync();

        var result = _mapper.Map<List<CustomerResponseDto>>(CustQuery);
        var totalCount = (from Cst in CustQuery select Cst).Count();
        var totalpages = (int)Math.Ceiling(totalCount / 25.00);

        return new FetchCustomersResponseDto() { Content = result, CurrentPage = pgNum, TotalPages = totalpages };
    }


    public async Task<OperationResponse> UpdateCustomer(int id, EditCustomerRequestDto request)
      {
         if (id < 1)
         return new OperationResponse() 
         { Msg = " يرجى ادخال رقم العميل", StatusCode = HttpStatusCode.BadRequest };

        if (request == null) 
            return new OperationResponse() 
            {Msg="يرجى إدخال بيانات العميل المطلوبة" ,StatusCode=HttpStatusCode.BadRequest};

        var OldCustomer = await (from Cust in _dbContext.Customers
                                 where Cust.Id == id 
                                 
                                 select Cust)
                                 .SingleOrDefaultAsync();
        if (OldCustomer == null)
            return new OperationResponse()
            { Msg = " thears no customer with this id", StatusCode = HttpStatusCode.BadRequest };


        if (OldCustomer.Name != request.Name)
        {
            var NameValidet = await (from Cust in _dbContext.Customers
                                     where Cust.Name == request.Name
                                     select Cust)
                                             .SingleOrDefaultAsync();
            if (NameValidet != null)
                return new OperationResponse()
                { Msg = "الاسم موجود مسبقًا", StatusCode = HttpStatusCode.BadRequest };
        } 
        

       

        _mapper.Map(request, OldCustomer);
      await  _dbContext.SaveChangesAsync();
        
        return new OperationResponse() { Msg ="Edited ",StatusCode = HttpStatusCode.OK};
        }
    public async Task<OperationResponse> DeleteCustomer(int id)
    {
        if (id is <= 0 )
            return new OperationResponse()
        { Msg = "الرجاء ادخال رقم عميل صحيح وموجود فعلًا",
            StatusCode = HttpStatusCode.BadRequest };
        var Cust = await (from customer in _dbContext.Customers
                          where customer.Id == id
                          && customer.Status == GeneralStatus.Active
                          select customer).FirstOrDefaultAsync();

        if (Cust == null)
            return new OperationResponse()
            {
                Msg = "عفوًا لا وجود لعميل بهذا الرقم",
                StatusCode = HttpStatusCode.NotFound
            };
        Cust.Status = GeneralStatus.Deleted;
        await _dbContext.SaveChangesAsync();
        return new OperationResponse()
        {
            Msg = Cust.Name +" لقد تم حذف العميل: "+" بنجاح! ",
            StatusCode = HttpStatusCode.OK
        };

    }
    public async Task<OperationResponse> LockCustomer(int id)
    {
        if (id is <= 0)
            return new OperationResponse()
            {
                Msg = "الرجاء ادخال رقم عميل صحيح وموجود فعلًا",
                StatusCode = HttpStatusCode.BadRequest
            };

        var Cust = await(from customer in _dbContext.Customers
                         where customer.Id == id
                         && customer.Status == GeneralStatus.Active
                         select customer).FirstOrDefaultAsync();

        if (Cust == null)
        {
            var LockedCustomer = await (from customer in _dbContext.Customers
                              where customer.Id == id
                              && customer.Status == GeneralStatus.LockedByUser
                              select customer).FirstOrDefaultAsync();
            if (LockedCustomer == null) 
            {
                return new OperationResponse()
                {
                    Msg = "عفوًا لا وجود لعميل بهذا الرقم",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            return new OperationResponse()
            {
                Msg = "عفوًا العميل مقفل مسبقًا !",
                StatusCode = HttpStatusCode.NotFound
            };

        }
        

        Cust.Status = GeneralStatus.LockedByUser;
        await _dbContext.SaveChangesAsync();
        return new OperationResponse()
        {
            Msg = Cust.Name + " لقد تم قفل العميل: " + " بنجاح! ",
            StatusCode = HttpStatusCode.OK
        };
    }

    public async Task<OperationResponse> UnlockCustomer(int id)
    {

        if (id is <= 0)
            return new OperationResponse()
            {
                Msg = "الرجاء ادخال رقم عميل صحيح وموجود فعلًا",
                StatusCode = HttpStatusCode.BadRequest
            };

        var Cust = await (from customer in _dbContext.Customers
                          where customer.Id == id
                          && customer.Status == GeneralStatus.LockedByUser
                          select customer).FirstOrDefaultAsync();

        if (Cust == null)
        {
            var ActiveCustomer = await (from customer in _dbContext.Customers
                                        where customer.Id == id
                                        && customer.Status == GeneralStatus.Active
                                        select customer).FirstOrDefaultAsync();
            if (ActiveCustomer == null)
                return new OperationResponse()
                {
                    Msg = "عفوًا لا وجود لعميل بهذا الرقم",
                    StatusCode = HttpStatusCode.NotFound
                };
            return new OperationResponse()
            {
                Msg = "العميل ليس مقفلا يرجى التأكد من الرقم !",
                StatusCode = HttpStatusCode.NotFound
            };
        }
          Cust.Status = GeneralStatus.Active;
          await _dbContext.SaveChangesAsync();

        return new OperationResponse()
        {
            Msg = "تم فتح القفل عن العميل  بنجاح !",
            StatusCode = HttpStatusCode.OK
        };
    }
   
}


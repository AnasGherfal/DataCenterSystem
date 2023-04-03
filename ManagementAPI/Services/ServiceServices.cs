using Infrastructure;
using Infrastructure.Models;
using ManagementAPI.Dtos.Service;
using Shared.Dtos;
using System.Net;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Common.Constants;
using Azure.Core;
using System;
using ManagementAPI.Dtos.Subscriptions;

namespace ManagementAPI.Services;

public class ServiceServices
{
    private readonly DataCenterContext _dbContext;
    private readonly IMapper _mapper;

    public ServiceServices(DataCenterContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<OperationResponse> CreateService(CreateServiceDto request)
    {
        //todo: check if exist
        if (await _dbContext.Services.AnyAsync(p => p.Name.ToLower() == request.Name.ToLower()))
        {
            return new OperationResponse()
            {
                Msg = "thier is simellar name stored in database",
                StatusCode = HttpStatusCode.BadRequest,
            };
        }
        var newservice = _mapper.Map<Service>(request);
        newservice.CreatedOn = DateTime.Now;
        newservice.CreatedById = 3;
        newservice.Status = 1;

        await _dbContext.Services.AddAsync(newservice);

        await _dbContext.SaveChangesAsync();
        return new OperationResponse() { Msg = "ok", StatusCode = HttpStatusCode.OK };
    }

    public async Task<FetchServicesResponseDto> GetAllService(int pagenum, int pagesize)
    {
        /*
        var Service_Query = await (from serv in _dbContext.Customers
                                   .OrderBy(x => x.Id)
                                    select serv)
*/
        // Queryable.Status=  (short)GeneralStatus.Deleted
        var Service_Query = await _dbContext.Services.Where(p => p.Status != 3)
  
        .OrderBy(p => p.Id)
        .Skip(pagesize * (pagenum - 1))
        .Take(pagesize)
        .ToListAsync();

        var result = _mapper.Map<List<ServiceResponseDto>>(Service_Query);
        var totalCount = (from serv in Service_Query select serv).Count();
        var totalpages = (int)Math.Ceiling(totalCount / 25.00);
        return new FetchServicesResponseDto() { Content = result, CurrentPage = pagenum, TotalPages = pagesize };



    }

    public async Task<OperationResponse> EditService(int id, UpdateServiceDto updateServiceDto)
    {
        if (updateServiceDto == null) return new OperationResponse()
        {
            Msg = "enter data",
            StatusCode = HttpStatusCode.BadRequest,



        };

        var update_query = await (from s in _dbContext.Services
                                  where s.Id == id
                                  select s).SingleOrDefaultAsync();

        if (update_query == null) return
               new OperationResponse()
               {
                   Msg = "no service with this id stored in database",
                   StatusCode = HttpStatusCode.BadRequest,


               };

        if (update_query.Name != updateServiceDto.Name)
        {
            var NameValidet = await (from Cust in _dbContext.Services
                                     where Cust.Name == updateServiceDto.Name
                                     select Cust)
                                             .SingleOrDefaultAsync();
            if (NameValidet != null)
                return new OperationResponse()
                { Msg = "الاسم موجود مسبقًا", StatusCode = HttpStatusCode.BadRequest };
        }

        _mapper.Map(updateServiceDto, update_query);
        await _dbContext.SaveChangesAsync();

        return new OperationResponse() { Msg = "ok edit", StatusCode = HttpStatusCode.OK };

    }

    public async Task<OperationResponse> RemoveService(int id)
    {
        if (id<0)
        {
            return new OperationResponse() { StatusCode = HttpStatusCode.BadRequest, Msg = "erorr in enter id " };

        }

        var remove_serv = await _dbContext.Services.FirstOrDefaultAsync(a=>a.Id==id);

        if (remove_serv == null) return new OperationResponse()
        { StatusCode = HttpStatusCode.BadRequest, Msg = "thear is no service with this id " };

        remove_serv.Status =(short)GeneralStatus.Deleted;
        await _dbContext.SaveChangesAsync();

        return new OperationResponse() {StatusCode=HttpStatusCode.OK,Msg="service is removed"};

    }

  
}



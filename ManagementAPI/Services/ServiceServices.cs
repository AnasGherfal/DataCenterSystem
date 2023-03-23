using Infrastructure;
using Infrastructure.Models;
using ManagementAPI.Dtos.Service;
using Shared.Dtos;
using System.Net;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Azure.Core;
using System;

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

    public async Task<FetchServicesResponseDto> GetAllService(int pagenum , int pagesize)
    {
        /*
        var Service_Query = await (from serv in _dbContext.Customers
                                   .OrderBy(x => x.Id)
                                    select serv)
*/

        var Service_Query = await _dbContext.Services
.OrderBy(p => p.Id)
        .Skip(pagesize * (pagenum - 1))
        .Take(pagesize)
        .ToListAsync();

        var getservices = _mapper.Map<List<ServiceResponseDto>>(Service_Query);
        var totalCount = (from serv in Service_Query select serv).Count();
        var totalpages = (int)Math.Ceiling(totalCount / 25.00);
        return new FetchServicesResponseDto() {Content = getservices,CurrentPage = pagenum,TotalPages = pagesize };
        
   

    } 

}


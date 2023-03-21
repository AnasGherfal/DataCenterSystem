using Infrastructure;
using Infrastructure.Models;
using ManagementAPI.Dtos.Service;
using Shared.Dtos;
using System.Net;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ManagementAPI.Services;

public class ServiceServices
{
    private readonly DataCenterContext _dbContext;
    private readonly IMapper _mapper;

    public ServiceServices(DataCenterContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<OperationResponse> CreateService(CreateServiceDto request)
    {
        //todo: check if exist
        var list = await _dbContext.Services.ToListAsync();
        await _dbContext.Services.AddAsync(new Service()
        {
            Name = request.Name,
            AcpPort = request.AcpPort,
            AmountOfPower = request.AmountOfPower,
            Dns = request.Dns,
            Price = request.Price,
            MonthlyVisits = request.MonthlyVisits,
            
        });
        await _dbContext.SaveChangesAsync();
        return new OperationResponse() { Msg = "ok", StatusCode = HttpStatusCode.OK };
    }
}

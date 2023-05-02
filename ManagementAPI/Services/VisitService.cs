﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infrastructure;
using Infrastructure.Models;
using ManagementAPI.Dtos.Customer;
using ManagementAPI.Dtos.Visit;
using Microsoft.EntityFrameworkCore;
using Shared.Constants;
using Shared.Dtos;
using System.Net;

namespace ManagementAPI.Services;

public class VisitService : IVisitService
{
    private readonly IMapper _mapper;
    private readonly DataCenterContext _dbContext;
    public VisitService(IMapper mapper, DataCenterContext dbContext)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<OperationResponse> Create(CreateVisitRequestDto request)
    {
        var data = _mapper.Map<Visit>(request);
        if (data == null)
            return new OperationResponse()
            { Msg = "! طلبك غير صالح يرجى إعادة المحاولة", StatusCode = HttpStatusCode.BadRequest };
        
        await _dbContext.Visits.AddAsync(data);
        await _dbContext.SaveChangesAsync();
        return new OperationResponse()
        {
            Msg = "Ok",
            StatusCode = HttpStatusCode.OK
        };
    
}

    public Task<OperationResponse> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<FetchVisitResponseDto> GetAll(FetchVisitRequestDto request)
    {
        var query = _dbContext.Visits
                .Where(p => p.InvoiceId == 0);

        var queryResult = await query
                              .Skip(request.PageSize * (request.PageNumber - 1))
                              .Take(request.PageSize)
                              .ToListAsync();
        var totalCount = query.Count();
        var totalpages = Math.Ceiling(totalCount / (double)request.PageSize);
        return new FetchVisitResponseDto()
        {
            Content =_mapper.Map<IList<VisitResponseDto>>(queryResult),
            CurrentPage = request.PageNumber,
            TotalPages = (int)totalpages
        };
    }

    public Task<OperationResponse> Lock(int id)
    {
        throw new NotImplementedException();
    }

    public Task<OperationResponse> Paid(int id, int InvoiceId)
    {
        throw new NotImplementedException();
    }

    public Task<OperationResponse> Unlock(int id)
    {
        throw new NotImplementedException();
    }

    public Task<OperationResponse> Update(int id, UpdateVisitRequestDto request)
    {
        throw new NotImplementedException();
    }
}

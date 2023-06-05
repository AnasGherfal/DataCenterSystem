using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Constants;
using Infrastructure;
using Infrastructure.Models;

using ManagementAPI.Dtos.Representative;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using Shared.Constants;
using Shared.Dtos;
using System.Linq;
using System.Net;
using Infrastructure.Constants;
using System.Diagnostics.Metrics;
using Shared.Exceptions;

namespace ManagementAPI.Services;

public class RepresentativeService : IRepresentativeService
{
    private readonly DataCenterContext _dbContext;
    private readonly IMapper _mapper;
    public RepresentativeService(DataCenterContext dbcontext, IMapper mapper)
    {
        _dbContext = dbcontext;
        _mapper = mapper;
    }
    public async Task<MessageResponse> Create(CreateRepresentativeRequestDto request)
    {
        var NewRepresentative = _mapper.Map<Representative>(request);
        var customer =await _dbContext.Customers.Include(p => p.Representatives).Where(p => p.Id == request.CustomerId).SingleOrDefaultAsync();
        if (customer == null)
            throw new BadHttpRequestException("عذرًا رقم العميل الذي أدخلته غير صحيح يرجى إعادة المحاولة");
        var RepresentativeCount = customer.Representatives.Count;
        if (RepresentativeCount >= 2)
            throw new BadHttpRequestException("عذرًا هذا العميل لديه الحد الأقصى من عدد المخوليين");
        _dbContext.Representatives.Add(NewRepresentative);
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "تمت اضافة المخول بنجاح"
        };
    }

    public async Task<RepresentativeResponseDto> GetById(int id, int customerId)
    {
        var data =await _dbContext.Representatives.Where(p => p.Id == id && p.CustomerId == customerId)   
            .ProjectTo<RepresentativeResponseDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
        if (data == null) throw new NotFoundException("عذرًا لا وجود لعميل أو مخول بهذا الرقم يرجى التأكد وإعادة المحاولة");
        return data;
    }
    public async Task<FetchRepresentativeResponseDto> GetAll(FetchRepresentativeRequestDto request)
    {
        var query = _dbContext.Representatives
                       .ProjectTo<RepresentativeResponseDto>(_mapper.ConfigurationProvider)
                       .Where(p => p.Status != GeneralStatus.Deleted);
        var resultQuery = await query.Skip(request.PageSize * (request.PageNumber - 1))
                                   .Take(request.PageSize)
                                   .ToListAsync();
        var totalCount = query.Count();
        var totalpages = Math.Ceiling(totalCount / (double)request.PageSize);
        return new FetchRepresentativeResponseDto()
        {
            Content = resultQuery,
            CurrentPage = request.PageNumber,
            TotalPages = (int)totalpages
        };
    }
    public async Task<MessageResponse> Delete(int id)
    {
        if (id <= 0)
           throw new BadHttpRequestException("الرجاء ادخال رقم مخول صحيح وموجود فعلًا");    
        var Representative = _dbContext.Representatives
                                      .Where(p => p.Id == id && p.Status == GeneralStatus.Active && p.Customer.Status==GeneralStatus.Active)
                                      .FirstOrDefault();
        if (Representative == null)
           throw new NotFoundException("! عذرًا..لا وجود لمخول بهذا الرقم");     
        Representative.Status = GeneralStatus.Deleted;
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "!بنجاح " + Representative.FullName + " : لقد تم حذف المخول",
        };
    }
    public async Task<MessageResponse> Lock(int id)
    {
        if (id <= 0)
            throw new BadHttpRequestException("الرجاء ادخال رقم مخول صحيح وموجود فعلًا");
        var Representative = await _dbContext.Representatives
                                           .Where(p => p.Id == id && p.Status != GeneralStatus.Deleted)
                                           .FirstOrDefaultAsync();
        if (Representative == null)
            throw new NotFoundException("! عذرًا..لا وجود لمخول بهذا الرقم");
        if (!IsLocked(Representative.Status))
        {
            Representative.Status = GeneralStatus.LockedByUser;
            await _dbContext.SaveChangesAsync();
            return new MessageResponse()
            {
                Msg = "!بنجاح " + Representative.FullName + " : لقد تم تقييد المخول",
            };
        }
        else
            throw new BadHttpRequestException(" ! عذرًا .. هذا المخول مقيد مسبقًا");
    }
    public async Task<MessageResponse> Unlock(int id)
    {
        if (id <= 0)
            throw new BadHttpRequestException("الرجاء ادخال رقم مخول صحيح وموجود فعلًا");
        var Representative = await _dbContext.Representatives.Where(p => p.Id == id && p.Status != GeneralStatus.Deleted)
                                                         .FirstOrDefaultAsync();
        if (Representative == null)
            throw new NotFoundException("! عذرًا..لا وجود لمخول بهذا الرقم");
        if (IsLocked(Representative.Status))
        {
            Representative.Status = GeneralStatus.Active;
            await _dbContext.SaveChangesAsync();
            return new MessageResponse()
            {
                Msg ="بنجاح " +Representative.FullName+ " : لقد تم إلغاء التقييد عن المخول",
            };
        }
        else
            throw new BadHttpRequestException( " ! عذرًا .. هذا المخول غير مقيد " );
    }

    public async Task<MessageResponse> Update(int id, UpdateRepresentativeRequestDto request)
    {
        if (id <= 0)
            throw new BadHttpRequestException("الرجاء ادخال رقم مخول صحيح وموجود فعلًا");
        var Representative = await _dbContext.Representatives
                                           .Where(p => p.Id == id && p.Status != GeneralStatus.Deleted)
                                           .FirstOrDefaultAsync();
        if (Representative == null)
            throw new NotFoundException("! عذرًا..لا وجود لمخول بهذا الرقم");
        if (IsLocked(Representative.Status))
            throw new BadHttpRequestException("! عذرًا..هذا المخول مقيد لا يمكنك تعديل بياناته ");
        _mapper.Map(request, Representative);
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "! تم تعديل المخول بنجاح",
        };
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
                return true;
        }
    }
}

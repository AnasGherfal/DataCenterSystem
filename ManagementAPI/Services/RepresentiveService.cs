using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Constants;
using Infrastructure;
using Infrastructure.Models;

using ManagementAPI.Dtos.Representive;
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

public class RepresentiveService : IRepresentiveService
{
    private readonly DataCenterContext _dbContext;
    private readonly IMapper _mapper;
    public RepresentiveService(DataCenterContext dbcontext, IMapper mapper)
    {
        _dbContext = dbcontext;
        _mapper = mapper;
    }
    public async Task<MessageResponse> Create(CreateRepresentiveRequestDto request)
    {
        var NewRepresentive = _mapper.Map<Representive>(request);
        var customer =await _dbContext.Customers.Include(p => p.Representives).Where(p => p.Id == request.CustomerId).SingleOrDefaultAsync();
        if (customer == null)
            throw new BadHttpRequestException("عذرًا رقم العميل الذي أدخلته غير صحيح يرجى إعادة المحاولة");
        var representiveCount = customer.Representives.Count;
        if (representiveCount >= 2)
            throw new BadHttpRequestException("عذرًا هذا العميل لديه الحد الأقصى من عدد المخوليين");
        _dbContext.Representives.Add(NewRepresentive);
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "تمت اضافة المخول بنجاح"
        };
    }

    public async Task<RepresentiveResponseDto> GetById(int id, int customerId)
    {
        var data = _dbContext.Representives.Where(p => p.Id == id && p.CustomerId == customerId).FirstOrDefault();
        if (data == null) throw new NotFoundException("عذرًا لا وجود لعميل أو مخول بهذا الرقم يرجى التأكد وإعادة المحاولة");
        var representive = _mapper.Map<RepresentiveResponseDto>(data);
        return representive;
    }
    public async Task<FetchRepresentiveResponseDto> GetAll(FetchRepresentiveRequestDto request)
    {
        var query = _dbContext.Representives
                       .ProjectTo<RepresentiveResponseDto>(_mapper.ConfigurationProvider)
                       .Where(p => p.Status != GeneralStatus.Deleted);
        var resultQuery = await query.Skip(request.PageSize * (request.PageNumber - 1))
                                   .Take(request.PageSize)
                                   .ToListAsync();
        var totalCount = query.Count();
        var totalpages = Math.Ceiling(totalCount / (double)request.PageSize);
        return new FetchRepresentiveResponseDto()
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
        var representive = _dbContext.Representives
                                      .Where(p => p.Id == id && p.Status == GeneralStatus.Active)
                                      .FirstOrDefault();
        if (representive == null)
           throw new NotFoundException("! عذرًا..لا وجود لمخول بهذا الرقم");     
        representive.Status = GeneralStatus.Deleted;
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "!بنجاح " + representive.FullName + " : لقد تم حذف المخول",
        };
    }
    public async Task<MessageResponse> Lock(int id)
    {
        if (id <= 0)
            throw new BadHttpRequestException("الرجاء ادخال رقم مخول صحيح وموجود فعلًا");
        var representive = await _dbContext.Representives
                                           .Where(p => p.Id == id && p.Status != GeneralStatus.Deleted)
                                           .FirstOrDefaultAsync();
        if (representive == null)
            throw new NotFoundException("! عذرًا..لا وجود لمخول بهذا الرقم");
        if (!IsLocked(representive.Status))
        {
            representive.Status = GeneralStatus.LockedByUser;
            await _dbContext.SaveChangesAsync();
            return new MessageResponse()
            {
                Msg = "!بنجاح " + representive.FullName + " : لقد تم تقييد المخول",
            };
        }
        else
            throw new BadHttpRequestException(" ! عذرًا .. هذا المخول مقيد مسبقًا");
    }
    public async Task<MessageResponse> Unlock(int id)
    {
        if (id <= 0)
            throw new BadHttpRequestException("الرجاء ادخال رقم مخول صحيح وموجود فعلًا");
        var representive = await _dbContext.Representives.Where(p => p.Id == id && p.Status != GeneralStatus.Deleted)
                                                         .FirstOrDefaultAsync();
        if (representive == null)
            throw new NotFoundException("! عذرًا..لا وجود لمخول بهذا الرقم");
        if (IsLocked(representive.Status))
        {
            representive.Status = GeneralStatus.Active;
            await _dbContext.SaveChangesAsync();
            return new MessageResponse()
            {
                Msg ="بنجاح " +representive.FullName+ " : لقد تم إلغاء التقييد عن المخول",
            };
        }
        else
            throw new BadHttpRequestException( " ! عذرًا .. هذا المخول غير مقيد " );
    }

    public async Task<MessageResponse> Update(int id, UpdateRepresentiveRequestDto request)
    {
        if (id <= 0)
            throw new BadHttpRequestException("الرجاء ادخال رقم مخول صحيح وموجود فعلًا");
        var representive = await _dbContext.Representives
                                           .Where(p => p.Id == id && p.Status != GeneralStatus.Deleted)
                                           .FirstOrDefaultAsync();
        if (representive == null)
            throw new NotFoundException("! عذرًا..لا وجود لمخول بهذا الرقم");
        if (IsLocked(representive.Status))
            throw new BadHttpRequestException("! عذرًا..هذا المخول مقيد لا يمكنك تعديل بياناته ");
        _mapper.Map(request, representive);
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

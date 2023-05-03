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
    public async Task<OperationResponse> Create(CreateRepresentiveRequestDto request)
    {
        var NewRepresentive = _mapper.Map<Representive>(request);
        _dbContext.Representives.Add(NewRepresentive);
        await _dbContext.SaveChangesAsync();
        return new OperationResponse()
        {
            Msg = "تمت اضافة المخول بنجاح",
            StatusCode = HttpStatusCode.OK
        };
    }
    public async Task<FetchRepresentiveResponseDto> GetAll(FetchRepresentiveRequestDto request)
    {
        var repQuery = _dbContext.Representives
                       .ProjectTo<RepresentiveResponseDto>(_mapper.ConfigurationProvider)
                       .Where(p => p.Status != GeneralStatus.Deleted);
        var result = await repQuery.Skip(request.PageSize * (request.PageNumber - 1))
                                   .Take(request.PageSize)
                                   .ToListAsync();
        var totalCount = repQuery.Count();
        var totalpages = Math.Ceiling(totalCount / (double)request.PageSize);
        return new FetchRepresentiveResponseDto()
        {
            Content = result,
            CurrentPage = request.PageNumber,
            TotalPages = (int)totalpages
        };
    }
    public async Task<OperationResponse> Delete(int id)
    {
        if (id <= 0)
            return new OperationResponse
            {
                Msg = "الرجاء ادخال رقم مخول صحيح وموجود فعلًا",
                StatusCode = HttpStatusCode.BadRequest
            };
        var representive = _dbContext.Representives
                                      .Where(p => p.Id == id && p.Status == GeneralStatus.Active)
                                      .FirstOrDefault();
        if (representive == null)
            return new OperationResponse
            {
                Msg = "! عذرًا..لا وجود لمخول بهذا الرقم",
                StatusCode = HttpStatusCode.BadRequest
            };
        representive.Status = GeneralStatus.Deleted;
        await _dbContext.SaveChangesAsync();
        return new OperationResponse
        {
            Msg = "!بنجاح " + representive.FullName + " : لقد تم حذف المخول",
            StatusCode = HttpStatusCode.OK
        };
    }
    public async Task<OperationResponse> Lock(int id)
    {
        if (id <= 0)
            return new OperationResponse()
            {
                Msg = "الرجاء ادخال رقم مخول صحيح وموجود فعلًا",
                StatusCode = HttpStatusCode.BadRequest
            };

        var representive = await _dbContext.Representives
                                           .Where(p => p.Id == id && p.Status != GeneralStatus.Deleted)
                                           .FirstOrDefaultAsync();
        if (representive == null)
            return new OperationResponse()
            {
                Msg = "! عذرًا..لا وجود لمخول بهذا الرقم",
                StatusCode = HttpStatusCode.BadRequest
            };
        if (!IsLocked(representive.Status))
        {
            representive.Status = GeneralStatus.LockedByUser;
            await _dbContext.SaveChangesAsync();
            return new OperationResponse()
            {
                Msg = "!بنجاح " + representive.FullName + " : لقد تم تقييد المخول",
                StatusCode = HttpStatusCode.OK
            };
        }
        else
            return new OperationResponse()
            {
                Msg = " ! عذرًا .. هذا المخول مقيد مسبقًا",
                StatusCode = HttpStatusCode.BadRequest
            };
    }
    public async Task<OperationResponse> Unlock(int id)
    {
        if (id <= 0)
            return new OperationResponse()
            {
                Msg = "الرجاء ادخال رقم مخول صحيح وموجود فعلًا",
                StatusCode = HttpStatusCode.BadRequest
            };
        var representive = await _dbContext.Representives.Where(p => p.Id == id && p.Status != GeneralStatus.Deleted)
                                                         .FirstOrDefaultAsync();
        if (representive == null)
            return new OperationResponse()
            {
                Msg = "! عذرًا..لا وجود لمخول بهذا الرقم",
                StatusCode = HttpStatusCode.BadRequest
            };
        if (IsLocked(representive.Status))
        {
            representive.Status = GeneralStatus.Active;
            await _dbContext.SaveChangesAsync();
            return new OperationResponse()
            {
                Msg ="بنجاح " +representive.FullName+ " : لقد تم إلغاء التقييد عن المخول",
                StatusCode = HttpStatusCode.OK
            };
        }
        else
            return new OperationResponse()
            {
                Msg = " ! عذرًا .. هذا المخول غير مقيد ",
                StatusCode = HttpStatusCode.BadRequest
            };
    }

    public async Task<OperationResponse> Update(int id, UpdateRepresentiveRequestDto request)
    {
        if (id <= 0)
            return new OperationResponse()
            {
                Msg = "الرجاء ادخال رقم مخول صحيح وموجود فعلًا",
                StatusCode = HttpStatusCode.BadRequest
            };
        var representive = await _dbContext.Representives
                                           .Where(p => p.Id == id && p.Status != GeneralStatus.Deleted)
                                           .FirstOrDefaultAsync();
        if (representive == null)
            return new OperationResponse()
            {
                Msg = "! عذرًا..لا وجود لمخول بهذا الرقم",
                StatusCode = HttpStatusCode.BadRequest
            };
        if (IsLocked(representive.Status))
            return new OperationResponse()
            {
                Msg = "! عذرًا..هذا المخول مقيد لا يمكنك تعديل بياناته ",
                StatusCode = HttpStatusCode.BadRequest
            };
        _mapper.Map(request, representive);
        await _dbContext.SaveChangesAsync();
        return new OperationResponse()
        {
            Msg = "! تم تعديل المخول بنجاح",
            StatusCode = HttpStatusCode.OK
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

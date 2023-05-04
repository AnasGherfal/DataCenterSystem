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
using Shared.Exceptions;

namespace ManagementAPI.Services;

public class RepresentiveService : IRepresentiveService
{
    private readonly DataCenterContext _dbContext;
    private readonly IMapper _mapper;
    public RepresentiveService(DataCenterContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<MessageResponse> Create(CreateRepresentiveRequestDto request)
    {
        //TODO: REVIEW [Warning]: Use camcelCasing for variable names.
        var NewRepresentive = _mapper.Map<Representive>(request);
        //TODO: REVIEW [Error]: Check For Uniqueness in IdentityNo, Phone, Email
        _dbContext.Representives.Add(NewRepresentive);
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "تمت اضافة المخول بنجاح",
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
        //TODO: REVIEW [Error]: Use CountAsync
        var totalCount = repQuery.Count();
        //TODO: REVIEW [Warning]: Use camcelCasing for variable names.
        var totalpages = Math.Ceiling(totalCount / (double)request.PageSize);
        return new FetchRepresentiveResponseDto()
        {
            Content = result,
            CurrentPage = request.PageNumber,
            //TODO: REVIEW [Bonus]: Include PageSize.
            TotalPages = (int)totalpages
        };
    }
    public async Task<MessageResponse> Delete(int id)
    {
        var representive = _dbContext.Representives
            .FirstOrDefault(p => p.Id == id && p.Status == GeneralStatus.Active);
        if (representive == null) throw new NotFoundException("! عذرًا..لا وجود لمخول بهذا الرقم");
        representive.Status = GeneralStatus.Deleted;
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "!بنجاح " + representive.FullName + " : لقد تم حذف المخول",
        };
    }
    
    public async Task<MessageResponse> Lock(int id)
    {
        var representive = await _dbContext.Representives
                                           .Where(p => p.Id == id && p.Status != GeneralStatus.Deleted)
                                           .FirstOrDefaultAsync();
        if (representive == null) throw new NotFoundException("! عذرًا..لا وجود لمخول بهذا الرقم");
       //TODO: REVIEW [Fatal]: Always use Early Return. (Leave Success return to LAST)
       if (representive.Status == GeneralStatus.LockedByUser) throw new BadRequestException(" ! عذرًا .. هذا المخول مقيد مسبقًا");
       // if (!IsLocked(representive.Status))
        // {
        //     representive.Status = GeneralStatus.LockedByUser;
        //     await _dbContext.SaveChangesAsync();
        //     return new OperationResponse()
        //     {
        //         Msg = "!بنجاح " + representive.FullName + " : لقد تم تقييد المخول",
        //         StatusCode = HttpStatusCode.OK
        //     };
        // }
        // else
        //     return new OperationResponse()
        //     {
        //         Msg = " ! عذرًا .. هذا المخول مقيد مسبقًا",
        //         StatusCode = HttpStatusCode.BadRequest
        //     };
        representive.Status = GeneralStatus.LockedByUser;
        await _dbContext.SaveChangesAsync();
        return new OperationResponse()
        {
            Msg = "!بنجاح " + representive.FullName + " : لقد تم تقييد المخول",
            StatusCode = HttpStatusCode.OK
        };
    }
    public async Task<MessageResponse> Unlock(int id)
    {
        var representive = await _dbContext.Representives
            .Where(p => p.Id == id && p.Status != GeneralStatus.Deleted)
             .FirstOrDefaultAsync();
        if (representive == null) throw new BadRequestException("! عذرًا..لا وجود لمخول بهذا الرقم");
        //TODO: REVIEW [Fatal]: Always use Early Return. (Leave Success return to LAST)
        if (representive.Status != GeneralStatus.LockedByUser) throw new BadRequestException(" ! عذرًا .. هذا المخول مقيد مسبقًا");
        // if (IsLocked(representive.Status))
        // {
        //     representive.Status = GeneralStatus.Active;
        //     await _dbContext.SaveChangesAsync();
        //     return new OperationResponse()
        //     {
        //         Msg ="بنجاح " +representive.FullName+ " : لقد تم إلغاء التقييد عن المخول",
        //         StatusCode = HttpStatusCode.OK
        //     };
        // }
        // else
        //     return new OperationResponse()
        //     {
        //         Msg = " ! عذرًا .. هذا المخول غير مقيد ",
        //         StatusCode = HttpStatusCode.BadRequest
        //     };
        representive.Status = GeneralStatus.Active;
        await _dbContext.SaveChangesAsync();
        return new OperationResponse()
        {
            Msg ="بنجاح " +representive.FullName+ " : لقد تم إلغاء التقييد عن المخول",
            StatusCode = HttpStatusCode.OK
        };
    }

    public async Task<MessageResponse> Update(int id, UpdateRepresentiveRequestDto request)
    {
        var representive = await _dbContext.Representives
                                           .Where(p => p.Id == id && p.Status != GeneralStatus.Deleted)
                                           .FirstOrDefaultAsync();
        if (representive == null) throw new BadRequestException("! عذرًا..لا وجود لمخول بهذا الرقم");
        if (representive.Status != GeneralStatus.Active) throw new BadRequestException("! عذرًا..هذا المخول مقيد لا يمكنك تعديل بياناته ");
        _mapper.Map(request, representive);
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "! تم تعديل المخول بنجاح",
        };
    }
}

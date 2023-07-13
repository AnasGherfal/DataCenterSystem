using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infrastructure;
using Infrastructure.Models;
using ManagementAPI.Dtos.Customer;
using ManagementAPI.Dtos.VisitTimeShift;
using Microsoft.EntityFrameworkCore;
using Shared.Constants;
using Shared.Dtos;
using System.Net;
using Infrastructure.Constants;

namespace ManagementAPI.Services;

public class VisitTimeShiftService : IVisitTimeShiftService
{
    private readonly IMapper _mapper;
    private readonly DataCenterContext _dbContext;
    public VisitTimeShiftService(IMapper mapper, DataCenterContext dbContext)
    {
        _dbContext = dbContext;     
        _mapper = mapper;
    }
    public async Task<OperationResponse> Create(CreateVisitTimeShiftRequestDto request)
    {
        var data = _mapper.Map<VisitTimeShift>(request);
        if (data == null)
            return new OperationResponse()
            {
                Msg = "يرجى التأكد من إدخال البيانات كاملة",
                StatusCode=HttpStatusCode.BadRequest
            };
        _dbContext.VisitTimeShifts.Add(data);
        await _dbContext.SaveChangesAsync();
        return new OperationResponse()
        {
            Msg="! تمت إضافة التوقيت بنجاح",
            StatusCode=HttpStatusCode.OK
        };
    }

    public async Task<OperationResponse> Delete(Guid id)
    {
        var query = await _dbContext.VisitTimeShifts.SingleOrDefaultAsync(p => p.Id == id && p.Status == GeneralStatus.Active);
        if (query == null) return new OperationResponse()
        {
            Msg = "يرجى التأكد من إدخال رقم توقيت صحيح",
            StatusCode = HttpStatusCode.BadRequest
        };
        query.Status = GeneralStatus.Deleted;
        await _dbContext.SaveChangesAsync();
        return new OperationResponse()
        {
            Msg = "تم حذف التوقيت",
            StatusCode = HttpStatusCode.BadRequest
        };
    }

    public async Task<FetchVisitTimeShiftResponseDto> GetAll(FetchVisitTimeShiftRequestDto request)
    {
        var query = _dbContext.VisitTimeShifts
                       .Where(p => p.Status != GeneralStatus.Deleted)
                       .ProjectTo<VisitTimeShiftResponseDto>(_mapper.ConfigurationProvider);
        var queryResult = await query.OrderBy(p => p.Name)
                              .Skip(request.PageSize * (request.PageNumber - 1))
                              .Take(request.PageSize)
                              .ToListAsync();
        var totalCount = query.Count();
        var totalpages = Math.Ceiling(totalCount / (double)request.PageSize);
        return new FetchVisitTimeShiftResponseDto()
        {
            Content = queryResult,
            CurrentPage = request.PageNumber,
            TotalPages = (int)totalpages
        };
    }

    public async Task<OperationResponse> Lock(Guid id)
    {
       

        var data = await _dbContext.VisitTimeShifts.Where(p => p.Id == id && p.Status == GeneralStatus.Active)
                                                 .FirstOrDefaultAsync();
        if (data == null)
            return new OperationResponse()
            {
                Msg = "! عذرًا..لا وجود لتوقيت زيارة بهذا الرقم",
                StatusCode = HttpStatusCode.BadRequest
            };

        if (!IsLocked(data.Status))
        {
            data.Status = GeneralStatus.LockedByUser;
            await _dbContext.SaveChangesAsync();

            return new OperationResponse()
            {
                Msg = "! بنجاح " + data.Name + " : لقد تم تقييد هذا التوقيت",
                StatusCode = HttpStatusCode.OK
            };
        }
        else
            return new OperationResponse()
            {
                Msg = " ! عذرًا .. هذا التوقيت مقيد مسبقًا",
                StatusCode = HttpStatusCode.BadRequest
            };
    }

    public async Task<OperationResponse> Unlock(Guid id)
    {
        

        var data = await _dbContext.VisitTimeShifts
                         .Where(p => p.Id == id && p.Status != GeneralStatus.Deleted)
                         .FirstOrDefaultAsync();
        if (data == null)
            return new OperationResponse()
            {
                Msg = "! عذرًا..لا وجود لتوقيت زيارة بهذا الرقم",
                StatusCode = HttpStatusCode.BadRequest
            };

        if (!IsLocked(data.Status))
            return new OperationResponse()
            {
                Msg = " ! عذرًا .. هذا التوقيت غير مقيد",
                StatusCode = HttpStatusCode.BadRequest
            };

        data.Status = GeneralStatus.Active;
        await _dbContext.SaveChangesAsync();
        return new OperationResponse()
        {
            Msg = "بنجاح " + data.Name + " :  تم إلغاء التقييد عن التوقيت",
            StatusCode = HttpStatusCode.OK
        };

    }
    public async Task<OperationResponse> Update(Guid id, UpdateVisitTimeShiftRequestDto request)
    {
       
        var data = await _dbContext.VisitTimeShifts
                         .Where(p => p.Id == id && p.Status != GeneralStatus.Deleted)
                         .FirstOrDefaultAsync();
        if (data == null)
            return new OperationResponse()
            {
                Msg = " يرجى التأكد من صحة رقم التوقيت",
                StatusCode = HttpStatusCode.BadRequest
            };
        if (data.Name != request.Name)
        {
            var isNotUnique = await _dbContext.VisitTimeShifts
                                    .Where(p => p.Name == request.Name && p.Status != GeneralStatus.Deleted)
                                    .AnyAsync();
            if (isNotUnique)
                return new OperationResponse()
                {
                    Msg = "هذا الاسم موجود مسبقًا",
                    StatusCode = HttpStatusCode.BadRequest
                };
        }
        _mapper.Map(request, data);
        await _dbContext.SaveChangesAsync();
        return new OperationResponse()
        {
            Msg = "! تم تعديل التوقيت بنجاح",
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
                return false;
        }
    }
}


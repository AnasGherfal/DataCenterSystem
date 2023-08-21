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
using Shared.Exceptions;

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
    public async Task<MessageResponse> Create(CreateVisitTimeShiftRequestDto request)
    {
        var data = _mapper.Map<VisitTimeShift>(request);
        if (data == null)
            throw new BadRequestException("يرجى التأكد من إدخال البيانات كاملة");
             
        _dbContext.VisitTimeShifts.Add(data);
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "! تمت إضافة التوقيت بنجاح"
        };
    }

    public async Task<MessageResponse> Delete(Guid id)
    {
        var query = await _dbContext.VisitTimeShifts.SingleOrDefaultAsync(p => p.Id == id && p.Status == GeneralStatus.Active)??
            throw new NotFoundException( "يرجى التأكد من إدخال رقم توقيت صحيح");
        query.Status = GeneralStatus.Deleted;
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "تم حذف التوقيت بنجاح"
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

    public async Task<MessageResponse> Lock(Guid id)
    {
       

        var data = await _dbContext.VisitTimeShifts.Where(p => p.Id == id && p.Status == GeneralStatus.Active)
                                                 .FirstOrDefaultAsync()?? throw new NotFoundException( "! عذرًا..لا وجود لتوقيت زيارة بهذا الرقم" );

        if (!IsLocked(data.Status))
        {
            data.Status = GeneralStatus.Locked;
            await _dbContext.SaveChangesAsync();

            return new MessageResponse()
            {
                Msg = "! بنجاح " + data.Name + " : لقد تم تقييد هذا التوقيت",
            };
        }
        else
            throw new BadRequestException(" ! عذرًا .. هذا التوقيت مقيد مسبقًا");
    }

    public async Task<MessageResponse> Unlock(Guid id)
    {


        var data = await _dbContext.VisitTimeShifts
                         .Where(p => p.Id == id && p.Status != GeneralStatus.Deleted)
                         .FirstOrDefaultAsync() ?? throw new NotFoundException("! عذرًا..لا وجود لتوقيت زيارة بهذا الرقم");

        if (!IsLocked(data.Status))
            throw new BadRequestException(" ! عذرًا .. هذا التوقيت غير مقيد");

        data.Status = GeneralStatus.Active;
        await _dbContext.SaveChangesAsync();
        return new MessageResponse() { Msg = "بنجاح " + data.Name + " :  تم إلغاء التقييد عن التوقيت" };

    }
    public async Task<MessageResponse> Update(Guid id, UpdateVisitTimeShiftRequestDto request)
    {
       
        var data = await _dbContext.VisitTimeShifts
                         .Where(p => p.Id == id && p.Status != GeneralStatus.Deleted)
                         .FirstOrDefaultAsync()?? throw new NotFoundException("يرجى التأكد من صحة رقم التوقيت");
        if (data.Name != request.Name)
        {
            var isNotUnique = await _dbContext.VisitTimeShifts
                                    .Where(p => p.Name == request.Name && p.Status != GeneralStatus.Deleted)
                                    .AnyAsync();
            if (isNotUnique)
               throw new BadRequestException("هذا الاسم موجود مسبقًا");
        }
        _mapper.Map(request, data);
        await _dbContext.SaveChangesAsync();
        return new MessageResponse() {  Msg = "! تم تعديل التوقيت بنجاح" };
    }
    private bool IsLocked(GeneralStatus status)
    {
        switch (status)
        {
            case GeneralStatus.Active:
                return false;
            case GeneralStatus.Locked:
                return true;
            default:
                return false;
        }
    }
}


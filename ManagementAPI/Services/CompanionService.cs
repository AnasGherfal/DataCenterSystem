using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infrastructure;
using Infrastructure.Models;
using ManagementAPI.Dtos.Companion;
using ManagementAPI.Dtos.Customer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared.Constants;
using Shared.Dtos;
using System.Net;

namespace ManagementAPI.Services;

public class CompanionService : ICompanionService
{
    private readonly DataCenterContext _dbContext;
    private readonly IMapper _mapper;
    public CompanionService(DataCenterContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext; 
        _mapper = mapper;
    }
    public async Task<OperationResponse> Create(CreateCompanionRequestDto request)
    {
        var data = _mapper.Map<Companion>(request);
        if (data == null)
            return new OperationResponse()
            { Msg = "! طلبك غير صالح يرجى إعادة المحاولة", StatusCode = HttpStatusCode.BadRequest };
       /* var isNotUnique = await _dbContext.Companions
                            .Where(p => p.FullName == data.FullName && p.VisitId == data.VisitId)
                            .AnyAsync();
        if (isNotUnique)
            return new OperationResponse()
            {
                Msg = "الاسم موجود مسبقًا",
                StatusCode = HttpStatusCode.BadRequest
            };*/
        await _dbContext.Companions.AddAsync(data);
        await _dbContext.SaveChangesAsync();
        return new OperationResponse()
        {
            Msg = "تمت إضافة المرافق بنجاح",
            StatusCode = HttpStatusCode.OK
        };
    }

    public async Task<OperationResponse> Delete(int id)
    {
        if (id < 1)
            return new OperationResponse()
            {
                Msg = " يرجى ادخال رقم مرافق صحيح",
                StatusCode = HttpStatusCode.BadRequest
            };
        var data = await _dbContext.Companions
                       .Where(p => p.Id == id)
                       .FirstOrDefaultAsync();
        if (data == null)
            return new OperationResponse()
            {
                Msg = "عفوًا لا وجود لمرافق بهذا الرقم",
                StatusCode = HttpStatusCode.NotFound
            };
        data.VisitId = 0;
        await _dbContext.SaveChangesAsync();
        return new OperationResponse()
        {
            Msg = "! بنجاح " + data.FullName + " : لقد تم حذف العميل",
            StatusCode = HttpStatusCode.OK
        };

    }

    public async Task<FetchCompanionResponseDto> GetAll(FetchCompanionRequestDto request)
    {
        var query = _dbContext.Companions
                          .Where(p => p.VisitId == request.VisitId)
                          .ProjectTo<CompanionResponseDto>(_mapper.ConfigurationProvider);
        var queryResult = await query.OrderBy(p => p.Id)
                              .Skip(request.PageSize * (request.PageNumber - 1))
                              .Take(request.PageSize)
                              .ToListAsync();
        var totalCount = query.Count();
        var totalpages = Math.Ceiling(totalCount / (double)request.PageSize);
        return new FetchCompanionResponseDto()
        {
            Content = queryResult,
            CurrentPage = request.PageNumber,
            TotalPages = (int)totalpages
        };
    }
    public async Task<OperationResponse> Update(int id, UpdateCompanionRequestDto request)
    {
        if (id < 1)
            return new OperationResponse()
            {
                Msg = "! يرجى ادخال رقم مرافق صحيح",
                StatusCode = HttpStatusCode.BadRequest
            };
        var data = await _dbContext.Companions
                         .Where(p => p.Id == id)
                         .FirstOrDefaultAsync();
        if (data == null)
            return new OperationResponse()
            {
                Msg = " يرجى التأكد من صحة رقم المرافق",
                StatusCode = HttpStatusCode.BadRequest
            };
        _mapper.Map(request, data);
        await _dbContext.SaveChangesAsync();
        return new OperationResponse()
        {
            Msg = "! تم تعديل المرافق بنجاح",
            StatusCode = HttpStatusCode.OK
        };
    }
}

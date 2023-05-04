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
using Shared.Exceptions;

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
    public async Task<MessageResponse> Create(CreateCompanionRequestDto request)
    {
        var data = _mapper.Map<Companion>(request);
        if (data == null) throw new BadRequestException("! طلبك غير صالح يرجى إعادة المحاولة");
        //TODO: REVIEW [Warning]: Don't commit commented code
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
        return new MessageResponse()
        {
            Msg = "تمت إضافة المرافق بنجاح"
        };
    }

    public async Task<MessageResponse> Delete(int id)
    {
        var data = await _dbContext.Companions
                       .Where(p => p.Id == id)
                       .FirstOrDefaultAsync();
        if (data == null) throw new NotFoundException("عفوًا لا وجود لمرافق بهذا الرقم");
        //TODO: REVIEW [Fatal]: Why?
        data.VisitId = 0;
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "! بنجاح " + data.FullName + " : لقد تم حذف العميل"
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
        //TODO: REVIEW [Error]: Use CountAsync
        var totalCount = query.Count();
        var totalPages = Math.Ceiling(totalCount / (double)request.PageSize);
        return new FetchCompanionResponseDto()
        {
            Content = queryResult,
            CurrentPage = request.PageNumber,
            //TODO: REVIEW [Warning]: Include PageSize
            TotalPages = (int)totalPages
        };
    }
    public async Task<MessageResponse> Update(int id, UpdateCompanionRequestDto request)
    {
        //TODO: REVIEW [Warning]: UpdateCompanionRequestDto JobTitle is not used?
        var data = await _dbContext.Companions
                         .Where(p => p.Id == id)
                         .FirstOrDefaultAsync();
        if (data == null) throw new NotFoundException(" يرجى التأكد من صحة رقم المرافق");
        _mapper.Map(request, data);
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "! تم تعديل المرافق بنجاح"
        };
    }
}

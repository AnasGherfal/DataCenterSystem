using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infrastructure;
using Infrastructure.Constants;
using Infrastructure.Models;
using ManagementAPI.Dtos.Customer;
using ManagementAPI.Dtos.Invoice;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Exceptions;

namespace ManagementAPI.Services;

public class InvoiceService : IInvoiceService
{
    private readonly IMapper _mapper;
    private readonly DataCenterContext _dbContext;
    public InvoiceService(DataCenterContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<MessageResponse> Create(CreateInvoiceRequestDto request)
    {
        var data = _mapper.Map<Invoice>(request)?? throw new BadRequestException("الرجاء التأكيد على ملئ البيانات بالكامل");
        var visits=await _dbContext.Visits.
            Where(p => (p.StartTime >= request.StartDate && p.EndTime <= request.EndDate)&& p.SubscriptionId==request.SubscriptionId).ToListAsync()?? throw new BadRequestException("عذرًا لا وجود لزيارات لهذا الإشتراك في المدة الزمنية المحددة");
        data.Visits= visits;

        foreach (var visit in visits) 
        {
            data.TotalAmount += visit.Price;
            visit.InvoiceId = data.Id;
            visit.Invoice = data;
            
            
        }
        _dbContext.Invoices.Add(data);
        await _dbContext.SaveChangesAsync();
        return new MessageResponse() 
        {
            Msg="تمت إضافة الفاتورة بنجاح"
        };
            
    }

    public Task<MessageResponse> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<FetchInvoicesResponseDto> GetAll(FetchInvoicesRequestDto request)
    {
        var query = _dbContext.Invoices
            .Include(p => p.Subscription)
            .Include(p => p.Visits)
            .ThenInclude(p => p.TimeShift)
            .Where(p => p.Status != GeneralStatus.Deleted);

        var queryResult = await query.OrderBy(p => p.Id)
            .Skip(request.PageSize * (request.PageNumber - 1))
            .Take(request.PageSize)
            .ProjectTo<InvoiceResponseDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        var totalCount = query.Count();
        var totalPages = Math.Ceiling(totalCount / (double)request.PageSize);
        return new FetchInvoicesResponseDto(request.PageNumber, (int)totalPages, queryResult);
    }

    public async Task<InvoiceResponseDto> GetById(int id)
    {
        if (id <= 0)
            throw new BadHttpRequestException("عذرًا رقم الفاتورة الذي أدخلته غير صالح!");
        var data = await _dbContext.Invoices.Where(p => p.Id == id && p.Status != GeneralStatus.Deleted).SingleOrDefaultAsync()?? throw new BadHttpRequestException("عذرًا لا وجود لفاتورة بهذا الرقم");
        var result = _mapper.Map<InvoiceResponseDto>(data);
        return result;
    }

    public Task<MessageResponse> Lock(int id)
    {
        throw new NotImplementedException();
    }

    public Task<MessageResponse> Unlock(int id)
    {
        throw new NotImplementedException();
    }

    public Task<MessageResponse> Update(int id, UpdateInvoiceRequestDto request)
    {
        throw new NotImplementedException();
    }
}

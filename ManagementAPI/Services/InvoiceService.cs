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
        var invoiceNo = GenerateInvoiceNo();
        var isNotUnique = await _dbContext.Invoices.AnyAsync(p => p.InvoiceNo == invoiceNo);
        while (isNotUnique)
        {
            invoiceNo = GenerateInvoiceNo();
            isNotUnique = await _dbContext.Invoices.AnyAsync(p => p.InvoiceNo == invoiceNo);
         }
        data.InvoiceNo = invoiceNo;
        _dbContext.Invoices.Add(data);
        await _dbContext.SaveChangesAsync();
        return new MessageResponse() 
        {
            Msg="تمت إضافة الفاتورة بنجاح"
        };
            
    }

    public async Task<MessageResponse> Delete(Guid id)
    {
        
        var data = await _dbContext.Invoices.Where(p => p.Id == id && p.Status != GeneralStatus.Deleted).SingleOrDefaultAsync() ??
            throw new BadHttpRequestException("عذرًا لا وجود لفاتورة بهذا الرقم");
        data.Status = GeneralStatus.Deleted;
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = $"بنجاح  {id} تم حذف الفاتورة "
        };
    }

    public async Task<FetchInvoicesResponseDto> GetAll(FetchInvoicesRequestDto request)
    {
        var query = _dbContext.Invoices
       .Include(p => p.Subscription)
       .ThenInclude(p => p.Customer)
       .Include(p => p.Visits)
       .ThenInclude(p => p.TimeShift)
       .Where(p => p.Status != GeneralStatus.Deleted);
        if (request.CustomerName != null)
                query = query.Where(p => p.Subscription.Customer.Name.Contains(request.CustomerName));
        if (request.StartDate != null && request.EndDate != null)
                query = query.Where(p => p.Date.Date >= request.StartDate.Value.Date && p.Date.Date <= request.EndDate.Value.Date);
        var queryResult = await query.OrderBy(p => p.Date)
            .Skip(request.PageSize * (request.PageNumber - 1))
            .Take(request.PageSize)
            .ProjectTo<InvoiceResponseDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        var totalCount = query.Count();
        var totalPages = Math.Ceiling(totalCount / (double)request.PageSize);
        return new FetchInvoicesResponseDto(request.PageNumber, (int)totalPages, queryResult);
    }

    public async Task<InvoiceResponseDto> GetById(Guid id)
    {
        
        var data = await _dbContext.Invoices.Where(p => p.Id == id && p.Status != GeneralStatus.Deleted)
            .Include(p => p.Subscription)
            .ThenInclude(p => p.Customer)
            .Include(p=>p.Visits)
            .ThenInclude(p => p.TimeShift)
            .SingleOrDefaultAsync()?? throw new BadHttpRequestException("عذرًا لا وجود لفاتورة بهذا الرقم");
        var result = _mapper.Map<InvoiceResponseDto>(data);
        return result;
    }

    public async Task<MessageResponse> Lock(Guid id)
    {
        var data = await _dbContext.Invoices.Where(p => p.Id == id && p.Status != GeneralStatus.Deleted).SingleOrDefaultAsync() ?? throw new BadHttpRequestException("عذرًا لا وجود لفاتورة بهذا الرقم");
        data.Status= GeneralStatus.Locked;
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = $"بنجاح  {id} تم تقييد الفاتورة "
        };
    }

    public async Task<MessageResponse> Unlock(Guid id)
    {
        var data = await _dbContext.Invoices.Where(p => p.Id == id && p.Status == GeneralStatus.Locked).SingleOrDefaultAsync() ?? throw new BadHttpRequestException("عذرًا لا وجود لفاتورة بهذا الرقم");
        data.Status = GeneralStatus.Active;
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = $"بنجاح  {id} تم فك تقييد الفاتورة "
    };
    }

    public async Task<MessageResponse> Update(Guid id, UpdateInvoiceRequestDto request)
    {
        var data = await _dbContext.Invoices.Where(p => p.Id == id && (p.Status != GeneralStatus.Locked  || p.Status != GeneralStatus.Deleted)).SingleOrDefaultAsync() ?? throw new BadHttpRequestException("عذرًا لا وجود لفاتورة بهذا الرقم");
        _mapper.Map(request, data);
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = $"بنجاح  {id} تم تعديل الفاتورة"
        };
        
    }

    private static string GenerateInvoiceNo()
    {
        string numbers = "1234567890";

        string characters = numbers;
        int length = 10;
        string id = string.Empty;
        for (int i = 0; i < length; i++)
        {
            string character = string.Empty;
            do
            {
                int index = new Random().Next(0, characters.Length);
                character = characters.ToCharArray()[index].ToString();
            } while (id.IndexOf(character) != -1);
            id += character;
        }
        
       return "FF" + id;
    }
}

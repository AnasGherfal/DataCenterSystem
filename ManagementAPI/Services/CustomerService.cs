using AutoMapper;
using Infrastructure;
using Infrastructure.Models;
using ManagementAPI.Dtos.Customer;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using System.Net;
using AutoMapper.QueryableExtensions;
using Infrastructure.Constants;
using Shared.Exceptions;

namespace ManagementAPI.Services;

public class CustomerService : ICustomerService
{
    private readonly DataCenterContext _dbContext;
    private readonly IMapper _mapper;

    public CustomerService(DataCenterContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<MessageResponse> Create(CreateCustomerRequestDto request)
    {
        var data = _mapper.Map<Customer>(request);
        if (data == null) throw new BadRequestException("! طلبك غير صالح يرجى إعادة المحاولة");
        var isNotUnique = await _dbContext.Customers
            .Where(p => p.Name == request.Name && p.Status != GeneralStatus.Deleted)
            .AnyAsync();
        if (isNotUnique) throw new BadRequestException("الاسم موجود مسبقًا");
        await _dbContext.Customers.AddAsync(data);
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "تمت إضافة العميل بنجاح!",
        };
    }

    public async Task<FetchCustomersResponseDto> GetAll(FetchCustomersRequestDto request)
    {
        var query = _dbContext.Customers
            .ProjectTo<CustomerResponseDto>(_mapper.ConfigurationProvider)
            .Where(p => p.Status != GeneralStatus.Deleted);
        var queryResult = await query.OrderBy(p => p.Id)
            .Skip(request.PageSize * (request.PageNumber - 1))
            .Take(request.PageSize)
            .ToListAsync();
        var totalCount = query.Count();
        var totalpages = Math.Ceiling(totalCount / (double)request.PageSize);
        return new FetchCustomersResponseDto()
        {
            Content = queryResult,
            CurrentPage = request.PageNumber,
            TotalPages = (int)totalpages
        };
    }

    public async Task<MessageResponse> Update(int id, UpdateCustomerRequestDto request)
    {
        var data = await _dbContext.Customers
            .Where(p => p.Id == id && p.Status != GeneralStatus.Deleted)
            .FirstOrDefaultAsync();
        if (data == null) throw new BadRequestException(" يرجى التأكد من صحة رقم العميل");
        if (data.Name != request.Name)
        {
            var isNotUnique = await _dbContext.Customers
                .Where(p => p.Name == request.Name && p.Status != GeneralStatus.Deleted)
                .AnyAsync();
            if (isNotUnique) throw new BadRequestException("الاسم موجود مسبقًا");
        }
        _mapper.Map(request, data);
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "! تم تعديل العميل بنجاح"
        };
    }

    public async Task<MessageResponse> Delete(int id)
    {
        var data = await _dbContext.Customers
            .Where(p => p.Id == id && p.Status == GeneralStatus.Active)
            .FirstOrDefaultAsync();
        if (data == null) throw new NotFoundException("عفوًا لا وجود لعميل بهذا الرقم");
        data.Status = GeneralStatus.Deleted;
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "! بنجاح " + data.Name + " : لقد تم حذف العميل",
        };
    }

    public async Task<MessageResponse> Lock(int id)
    {
        var data = await _dbContext.Customers
            .Where(p => p.Id == id && p.Status == GeneralStatus.Active)
            .FirstOrDefaultAsync();
        if (data == null) throw new NotFoundException("! عذرًا..لا وجود لعميل بهذا الرقم او هذا العميل مقيد مسبقًا");
        data.Status = GeneralStatus.LockedByUser;
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "! بنجاح " + data.Name + " : لقد تم تقييد العميل"
        };
    }

    public async Task<MessageResponse> Unlock(int id)
    {
        var data = await _dbContext.Customers
            .Where(p => p.Id == id && p.Status != GeneralStatus.LockedByUser)
            .FirstOrDefaultAsync();
        if (data == null) throw new NotFoundException("! عذرًا..لا وجود لعميل بهذا الرقم او هذا العميل غير مقيد");
        data.Status = GeneralStatus.Active;
        await _dbContext.SaveChangesAsync();
        return new OperationResponse()
        {
            Msg = "بنجاح " + data.Name + " :  تم إلغاء التقييد عن العميل",
            StatusCode = HttpStatusCode.OK
        };
    }
}


using System.Net;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infrastructure;
using Infrastructure.Constants;
using Infrastructure.Models;
using ManagementAPI.Dtos.Customer;
using ManagementAPI.Dtos.Service;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Exceptions;

namespace ManagementAPI.Services;

public class ServiceServices : IServiceServices
{
    private readonly DataCenterContext _dbContext;
    private readonly IMapper _mapper;

    public ServiceServices(DataCenterContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<MessageResponse> Create(CreateServiceDto request)
    {
        if (await _dbContext.Services.AnyAsync(p => p.Name == request.Name && p.Status != GeneralStatus.Deleted))
            throw new BadRequestException("عذرًا ولكن هذا الأسم موجود مسبقًا");
        var data = _mapper.Map<Service>(request);
        await _dbContext.Services.AddAsync(data);
        await _dbContext.SaveChangesAsync();
        return new MessageResponse() { Msg = "تمت إضافة الباقة بنجاح" };
    }

    public async Task<ServiceResponseDto> GetById(Guid id)
    {
       
        var data = await _dbContext.Services.Where(p => p.Id == id && p.Status != GeneralStatus.Deleted)
                                       .ProjectTo<ServiceResponseDto>(_mapper.ConfigurationProvider)
                                       .SingleOrDefaultAsync() ?? throw new NotFoundException("عذرًا لا وجود لباقة بهذا الرقم يرجى التأكد من الرقم!");
        return data;
    }
    public async Task<FetchServicesResponseDto> GetAll(FetchServicesRequestDto request)
    {
        var query =  _dbContext.Services
            .Where(p => p.Status != GeneralStatus.Deleted);
        var queryResult = await query
            .ProjectTo<ServiceResponseDto>(_mapper.ConfigurationProvider)
            .OrderBy(p => p.Id)
            .Skip(request.PageSize * (request.PageNumber - 1))
            .Take(request.PageSize)
            .ToListAsync();
        
        var totalCount = await query.CountAsync();
        var totalpages = (int) Math.Ceiling(totalCount / (decimal) request.PageSize);
        return new FetchServicesResponseDto() {
            Content = queryResult,
            CurrentPage = request.PageNumber,
            TotalPages = totalpages,
        };
    }

    public async Task<MessageResponse> Update(Guid id, UpdateServiceDto request)
    {
        var data = await _dbContext.Services.SingleOrDefaultAsync(p => p.Id == id && p.Status != GeneralStatus.Deleted)
       ?? throw new NotFoundException("!عذرًا لا وجود لباقة بهذا الرقم");
        if(IsLocked(data.Status))
            throw new BadRequestException($"! {data.Id}: عذرًا هذه الباقة مقيدة");
        if (data.Name != request.Name)
        {
            var isNotUnique = await _dbContext.Services.Where(p => p.Name == request.Name && p.Status != GeneralStatus.Deleted).AnyAsync();
            if (isNotUnique)
            throw new BadRequestException("!عذرًا ولكن هذا الأسم محجوز لباقة أخرى");
        }
        _mapper.Map(request, data);
        await _dbContext.SaveChangesAsync();
        return new MessageResponse() 
        {
            Msg = "تم تعديل الباقة بنجاح"
            
        };
    }

    public async Task<MessageResponse> Delete(Guid id)
    {
        var data = await _dbContext.Services.FirstOrDefaultAsync(a => a.Id == id && a.Status != GeneralStatus.Deleted)
          ?? throw new NotFoundException("عذرًا لا وجود لباقة بهذا الرقم");
        if (IsLocked(data.Status))
            throw new BadRequestException($"! {data.Id}: عذرًا هذه الباقة مقيدة");
          
        data.Status = GeneralStatus.Deleted;
        await _dbContext.SaveChangesAsync();
        return new MessageResponse() 
        {
            
            Msg=$"بنجاح {data.Name} تم حذف الباقة "
        };

    }

  public async Task<MessageResponse> Lock(Guid id)
    {
        var data = await _dbContext.Services.FirstOrDefaultAsync(b => b.Id == id && b.Status != GeneralStatus.Deleted)
            ?? throw new NotFoundException("!عذرًا ولكن لا وجود لباقة بهذا الرقم");
        
        if(IsLocked(data.Status))
            throw new BadRequestException($"! {data.Id}: عذرًا هذه الباقة مقيدة مسبقًا"); 
        
        data.Status = GeneralStatus.Locked;
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = data.Name + " لقد تم تقييد الخدمة: " + " بنجاح! ",
            
        };

    }

  public async Task<MessageResponse> Unlock(Guid id)
  {
      
        var data = await _dbContext.Services.FirstOrDefaultAsync(
          b => b.Id == id && b.Status != GeneralStatus.Deleted)
        ?? throw new NotFoundException("! عذرًا لاوجود لباقة بهذا الرقم");
        if (!IsLocked(data.Status))
            throw new BadRequestException("! عذرًا ولكن هذه الباقة ليست مقيدة");
        data.Status = GeneralStatus.Active;
          return new MessageResponse()
          {
              Msg = "تم إلفاء التقييد عن هذه الباقة بنجاح ",
          };

      
 
  }

    private static bool IsLocked(GeneralStatus status)
    {
        return status switch
        {
            GeneralStatus.Active => false,
            GeneralStatus.Locked => true,
            _ => true,
        };
    }
}



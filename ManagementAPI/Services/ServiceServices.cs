using System.Net;
using AutoMapper;
using Infrastructure;
using Infrastructure.Constants;
using Infrastructure.Models;
using ManagementAPI.Dtos.Service;
using ManagementAPI.Dtos.Subscriptions;
using ManagementAPI.Dtos.User;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Exceptions;

namespace ManagementAPI.Services;

public class ServiceServices
{
    private readonly DataCenterContext _dbContext;
    private readonly IMapper _mapper;

    public ServiceServices(DataCenterContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<OperationResponse> Create(CreateServiceDto request)
    {
        if (await _dbContext.Services.AnyAsync(p => p.Name == request.Name && p.Status != GeneralStatus.Deleted))
        {
            return new OperationResponse()
            {
                Msg = "thier is simellar name stored in database",
                StatusCode = HttpStatusCode.BadRequest,
            };
        }
        var data = _mapper.Map<Service>(request);
        data.CreatedOn = DateTime.UtcNow;
        //todo: replace with user id when added
        data.CreatedById = 3;
        data.Status =  GeneralStatus.Active;
        await _dbContext.Services.AddAsync(data);
        await _dbContext.SaveChangesAsync();
        return new OperationResponse() { Msg = "ok", StatusCode = HttpStatusCode.OK };
    }

    public async Task<FetchServicesResponseDto> GetAll(FetchServicesRequestDto request)
    {
        var query =  _dbContext.Services
            .Where(p => p.Status != GeneralStatus.Deleted);
        var queryResult = await query.OrderBy(p => p.Id)
            .Skip(request.PageSize * (request.PageNumber - 1))
            .Take(request.PageSize)
            .ToListAsync();
        var result = _mapper.Map<List<ServiceResponseDto>>(queryResult);
        var totalCount = await query.CountAsync();
        var totalpages = (int) Math.Ceiling(totalCount / (decimal) request.PageSize);
        return new FetchServicesResponseDto() {
            Content = result,
            CurrentPage = request.PageNumber,
            PageSize = request.PageSize,
            TotalPages = totalpages,
        };
    }
    public async Task<ServiceResponseDto> GetById(int id)
    {
        if (id < 0) throw new BadRequestException("! طلبك غير صالح يرجى إعادة المحاولة");
        var query = await _dbContext.Services.SingleOrDefaultAsync(p => p.Id == id && p.Status != GeneralStatus.Deleted);
        if (query == null) throw new NotFoundException("there is no sevice with this number");
        var result = _mapper.Map<ServiceResponseDto>(query);
        return result;
    }
    public async Task<OperationResponse> Update(int id, UpdateServiceDto request)
    {
        var data = await _dbContext.Services.SingleOrDefaultAsync(p => p.Id == id && p.Status != GeneralStatus.Deleted );
        if (data == null) return
                new OperationResponse()
                {
                    Msg = "no service with this number",
                    StatusCode = HttpStatusCode.BadRequest
                };
        if (data.Status == GeneralStatus.LockedByUser) return
                new OperationResponse()
                {
                    Msg = "this service is locked by user you cannot updated",
                    StatusCode = HttpStatusCode.BadRequest
                };
        if (data.Name != request.Name)
        {
            var isNotUnique = await _dbContext.Services.Where(p => p.Name == request.Name && p.Status != GeneralStatus.Deleted).AnyAsync();
            if (isNotUnique) return new OperationResponse()
            { 
                Msg = "الاسم موجود مسبقًا",
                StatusCode = HttpStatusCode.BadRequest
            };
        }
        _mapper.Map(request, data);
        await _dbContext.SaveChangesAsync();
        return new OperationResponse() 
        {
            Msg = "ok edit", 
            StatusCode = HttpStatusCode.OK
        };
    }

    public async Task<OperationResponse> Remove(int id)
    {
        var data = await _dbContext.Services.FirstOrDefaultAsync(a => a.Id == id && a.Status != GeneralStatus.Deleted);
        if (data == null) return
                new OperationResponse()
        { StatusCode = HttpStatusCode.BadRequest,
            Msg = "thear is no service with this id "
        };
        if (data.Status == GeneralStatus.LockedByUser) return
               new OperationResponse()
               {
                   Msg = "this service is locked by user you cannot Removed",
                   StatusCode = HttpStatusCode.BadRequest
               };
        data.Status = GeneralStatus.Deleted;
        await _dbContext.SaveChangesAsync();
        return new OperationResponse() 
        {
            StatusCode=HttpStatusCode.OK,
            Msg="service is removed"
        };

    }

  public async Task<OperationResponse> Lock(int id)
    {
        if (id < 0)
            return new OperationResponse()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Msg = "incorrect service id"
            };
        var data = await _dbContext.Services.FirstOrDefaultAsync(b => b.Id == id && b.Status == GeneralStatus.Active);
        if(data == null)
        {
            var isLocked = await _dbContext.Services.Where(b => b.Id == id && b.Status == GeneralStatus.LockedByUser).AnyAsync();
            if (!isLocked)
            {
                return new OperationResponse()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Msg = "thear is no service with this number"
                };

            }
            return new OperationResponse()
            {
                Msg = "عفوًا الخدمة مقفله مسبقًا !",
                StatusCode = HttpStatusCode.NotFound
            };

        }
        data.Status = GeneralStatus.LockedByUser;
        await _dbContext.SaveChangesAsync();
        return new OperationResponse()
        {
            Msg = data.Name + " لقد تم قفل الخدمة: " + " بنجاح! ",
            StatusCode = HttpStatusCode.OK
        };



    }

  public async Task<OperationResponse> Unlock(int id)
  {
      if (id < 0)
          return new OperationResponse()
          {
              StatusCode = HttpStatusCode.BadRequest,
              Msg = "thear is no service with this number"
          };
      var data = await _dbContext.Services.FirstOrDefaultAsync(
          b => b.Id == id && b.Status == GeneralStatus.LockedByUser);
      if (data == null)
      {
          var isUnLocked = await _dbContext.Services.Where(b => b.Id == id && b.Status == GeneralStatus.Active)
              .AnyAsync();
          if (!isUnLocked)
          {
              return new OperationResponse()
              {
                  StatusCode = HttpStatusCode.BadRequest,
                  Msg = "thear is no service with this number"
              };

          }

          return new OperationResponse()
          {
              Msg = "عفوًا الخدمة غير مقفله مسبقًا !",
              StatusCode = HttpStatusCode.NotFound
          };

      }
      data.Status = GeneralStatus.Active;
      await _dbContext.SaveChangesAsync();
      return new OperationResponse()
      {
          Msg = data.Name + " تم فتح القفل عن الخدمة: " + " بنجاح! ",
          StatusCode = HttpStatusCode.OK
      };
  }
}



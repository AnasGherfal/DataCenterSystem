using AutoMapper;
using Infrastructure;
using Infrastructure.Models;
using ManagementAPI.Dtos.Customer;
using ManagementAPI.Dtos.Service;
using ManagementAPI.Dtos.Subscriptions;
using ManagementAPI.Dtos.File;
using Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using System.Net;
using Microsoft.IdentityModel.Tokens;
using Azure.Core;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Serilog;
using Microsoft.VisualBasic;
using Infrastructure.Constants;

namespace ManagementAPI.Services;

public class SubscriptionService
{
    private readonly DataCenterContext _dbContext;
    private readonly IMapper _mapper;
    public SubscriptionService(DataCenterContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<OperationResponse> CreateSubscription(CreateSubscriptionDto request)
    {
        if (request == null)
            return new OperationResponse()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Msg = "enter subscription data "
            };

        var newsubs = _mapper.Map<Subscription>(request);
        await _dbContext.Subscriptions.AddAsync(newsubs);
        await _dbContext.SaveChangesAsync();
        return new OperationResponse()
        {
            StatusCode = HttpStatusCode.OK,
            Msg = "ok"
        };

    }

    public async Task<FetchSubscriptionResponseDto> GetAllSubscription(int pagenum, int pagesize)
    {
        var Subs_Query = await _dbContext.Subscriptions
            .Include(a => a.Service)
            .Include(a => a.Customer)
            .OrderBy(p => p.Id)
            .Skip(pagesize * (pagenum - 1))
            .Take(pagesize)
            .ToListAsync();

        var result = _mapper.Map<List<SubscriptionRsponseDto>>(Subs_Query);
        var totalCount = (from Cst in Subs_Query select Cst).Count();
        var totalpages = (int)Math.Ceiling(totalCount / 25.00);

        return new FetchSubscriptionResponseDto()
        {
            Content = result,
            CurrentPage = pagenum,
            TotalPages = pagesize
        };

    }
    public async Task<OperationResponse> Renew(int id)
    {
        
        var data = await _dbContext.Subscriptions.SingleOrDefaultAsync(a => a.Id == id);


        if (data == null) return new OperationResponse()
        {
            StatusCode = HttpStatusCode.BadRequest,
            Msg = "no subscription with this number "
        };
        if(data.Status == GeneralStatus.LockedByUser)
        {
            return new OperationResponse()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Msg = "subsciption is locked by user you cannot renew "

            };
        }
        var datetimenow = DateTime.UtcNow;
        var isExpired = datetimenow.CompareTo(data.EndDate);
        if (datetimenow > data.EndDate)
        {
            data.EndDate = datetimenow.AddYears(1);
            await _dbContext.SaveChangesAsync();

            return new OperationResponse()
            {
                Msg = "   تم تجديدالخدمة بداية من تاريخ اليوم: " + " بنجاح! ",
                StatusCode = HttpStatusCode.OK
            };
        }
        var newdateNow = datetimenow.AddDays(30);
        datetimenow = newdateNow;
        var duration = datetimenow.CompareTo(data.EndDate);
        if (datetimenow >= data.EndDate)
        {
            var newEndDate = data.EndDate.AddYears(1);
            data.EndDate = newEndDate;
            await _dbContext.SaveChangesAsync();
            return new OperationResponse()
            {
                Msg = data.EndDate.ToString() + "   تم تجديدالخدمة بداية من تاريخ الانتهاء للخدمة: " + " بنجاح! ",
                StatusCode = HttpStatusCode.OK
            };
        }
        return new OperationResponse()
        {
            Msg = data.EndDate.ToString() + "   عذرا لم يتم تجديد الخدمة يمكنك التجديد عند انتهاء الخدمة او قبلها ب 30 يوما ",
            StatusCode = HttpStatusCode.BadRequest
        };
    }
    public async Task<OperationResponse> UploadFile(int id, FileDto file)
    {
        var data = await _dbContext.Subscriptions.SingleOrDefaultAsync(a => a.Id == id);
        if (data == null)
        {
            return new OperationResponse()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Msg = "thear is no subscription with this number"

            };
        }
        string fileName;
        var extention = "." + file.File.FileName.Split('.')[file.File.FileName.Split('.').Length - 1];
        fileName = id + "_" + DateTime.UtcNow.Ticks + file.File.FileName;
        var pathBuild = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files");
        if (!Directory.Exists(pathBuild))
        {
            Directory.CreateDirectory(pathBuild);
        }
        var path = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files\\" + fileName);
        using (var stream = new FileStream(path, FileMode.Create))
        {
            await file.File.CopyToAsync(stream);
        }
        SubscriptionFile newFile = new SubscriptionFile()
        {
            CreatedById = 3,
            CreatedOn = DateTime.UtcNow,
            FileName = path,
            FileType = extention,
            SubscriptionId = id,
        };
        await _dbContext.SubscriptionFiles.AddAsync(newFile);
        await _dbContext.SaveChangesAsync();
        data.SubscriptionFileId = newFile.Id;
        
        await _dbContext.SaveChangesAsync();
        return new OperationResponse()
        {
            StatusCode = HttpStatusCode.OK,
            Msg = "file uploaded"
        };
        
    }
    public async Task<FetchSubscriptionFileResponseDto> GetFile(FetchSubscriptionRequestDto request)
    {
       
           
            var queryResult = await _dbContext.SubscriptionFiles
                .OrderBy(p => p.Id)
                .Skip(request.PageSize * (request.PageNumber - 1))
                .Take(request.PageSize)
                .ToListAsync();
            var result = _mapper.Map<List<SubscriptionFileResponsDto>>(queryResult);
            var totalCount =  queryResult.Count();
            var totalpages = (int)Math.Ceiling(totalCount / (decimal)request.PageSize);
            return new FetchSubscriptionFileResponseDto()
            {
                Content = result,
                CurrentPage = request.PageNumber,
                PageSize = request.PageSize,
                TotalPages = totalpages,
            };
        }
    public async Task<SubscriptionFileResponsDto> GetFileBySubscriptionId(int id)
    {
        
        var data = await _dbContext.Subscriptions.SingleOrDefaultAsync(a => a.Id == id);
       
        var fileId = data.SubscriptionFileId;
        var fileData = await _dbContext.SubscriptionFiles.SingleOrDefaultAsync(a => a.Id == fileId);
       
        
        return new SubscriptionFileResponsDto() { Id = fileData.Id, FileName = fileData.FileName , FileType = fileData.FileType };
    }
    public async Task<OperationResponse> Lock(int id)
    {
        if (id < 0)
            return new OperationResponse()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Msg = "incorrect Subscription id"
            };
        var data = await _dbContext.Subscriptions.FirstOrDefaultAsync(b => b.Id == id && b.Status == GeneralStatus.Active);
        if (data == null)
        {
            var isLocked = await _dbContext.Subscriptions.Where(b => b.Id == id && b.Status == GeneralStatus.LockedByUser).AnyAsync();
            if (!isLocked)
            {
                return new OperationResponse()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Msg = "thear is no subscription with this number"
                };

            }
            return new OperationResponse()
            {
                Msg = "عفوًا الاشتراك مقفل مسبقًا !",
                StatusCode = HttpStatusCode.NotFound
            };

        }
        data.Status = GeneralStatus.LockedByUser;
        await _dbContext.SaveChangesAsync();
        return new OperationResponse()
        {
            Msg = data.Customer.Name + "  لقد تم قفل الاشتراك لهاذا العميل: " + " بنجاح! ",
            StatusCode = HttpStatusCode.OK
        };



    }
    public async Task<OperationResponse> Unlock(int id)
    {
        if (id < 0) return new OperationResponse()
        {
            StatusCode = HttpStatusCode.BadRequest,
            Msg = "incorrect Subscription id"
        };
        var data = await _dbContext.Subscriptions.FirstOrDefaultAsync(b => b.Id == id && b.Status == GeneralStatus.LockedByUser);
        if (data == null)
        {
            var isUnLocked = await _dbContext.Subscriptions.Where(b => b.Id == id && b.Status == GeneralStatus.Active).AnyAsync();
            if (!isUnLocked)
            {
                return new OperationResponse()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Msg = "thear is no subscription with this number"
                };

            }
            return new OperationResponse()
            {
                Msg = "عفوًا الاشتراك غير مقفل مسبقًا !",
                StatusCode = HttpStatusCode.NotFound
            };

        }
        data.Status = GeneralStatus.Active;
        await _dbContext.SaveChangesAsync();
        return new OperationResponse()
        {
            Msg = data.Customer.Name + " تم فتح القفل عن الاشتراك: " + " بنجاح! ",
            StatusCode = HttpStatusCode.OK
        };



    }
    public async Task<OperationResponse> Remove(int id)
    {
        if (id < 0) return new OperationResponse()
        {
            StatusCode = HttpStatusCode.BadRequest,
            Msg = "incorrect Subscription id"
        };
        var data = await _dbContext.Subscriptions.FirstOrDefaultAsync(a => a.Id == id && a.Status != GeneralStatus.Deleted);
        if (data == null) return
                new OperationResponse()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Msg = "thear is no subscription with this id "
                };
        if (data.Status == GeneralStatus.LockedByUser) return
               new OperationResponse()
               {
                   Msg = "this subscription is locked by user you cannot Removed",
                   StatusCode = HttpStatusCode.BadRequest
               };
        data.Status = GeneralStatus.Deleted;
        await _dbContext.SaveChangesAsync();
        return new OperationResponse()
        {
            StatusCode = HttpStatusCode.OK,
            Msg = "subscription is removed"
        };

    }
}
 

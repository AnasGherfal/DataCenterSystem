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
using Shared.Exceptions;

namespace ManagementAPI.Services;

//TODO: REVIEW [Consistency]: Use Interface For DI
public class SubscriptionService
{
    //TODO: REVIEW [Warning]: Response Messages need to be reviewed
    private readonly DataCenterContext _dbContext;
    private readonly IMapper _mapper;
    public SubscriptionService(DataCenterContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<MessageResponse> Create(CreateSubscriptionDto request)
    {
        var data = _mapper.Map<Subscription>(request);
        await _dbContext.Subscriptions.AddAsync(data);
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            //TODO: REVIEW: Use Sufficient Response Message.
            Msg = "ok"
        };
    }

    public async Task<FetchSubscriptionResponseDto> GetAll(int pageNumber, int pageSize)
    {
        //TODO: REVIEW [Warning]: Variable Naming, Use ProjectTo to avoid loading all data from DB
        var Subs_Query = await _dbContext.Subscriptions
            .Include(a => a.Service)
            .Include(a => a.Customer)
            .OrderBy(p => p.Id)
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();
        var result = _mapper.Map<List<SubscriptionRsponseDto>>(Subs_Query);
        var totalCount = (from Cst in Subs_Query select Cst).Count();
        var totalpages = (int)Math.Ceiling(totalCount / 25.00);
        return new FetchSubscriptionResponseDto()
        {
            Content = result,
            CurrentPage = pageNumber,
            //TODO: REVIEW [Error]: Using incorrect variable, PageSize Missing
            TotalPages = pageSize
        };

    }
    public async Task<MessageResponse> Renew(int id)
    {
        //TODO: REVIEW [Fatal]: data should not have a deleted status
        var data = await _dbContext.Subscriptions.SingleOrDefaultAsync(a => a.Id == id);
        if (data == null) throw new NotFoundException("no subscription with this number");
        if (data.Status == GeneralStatus.LockedByUser) throw new BadRequestException("subsciption is locked by user you cannot renew ");
        //TODO: REVIEW [Warning]: Variable Naming
        var datetimenow = DateTime.UtcNow;
        //TODO: REVIEW [Fatal]: Logic flow is too broad, Use Function instead with Clear Name
        var isExpired = datetimenow.CompareTo(data.EndDate);
        if (datetimenow > data.EndDate)
        {
            data.EndDate = datetimenow.AddYears(1);
            await _dbContext.SaveChangesAsync();
            //TODO: REVIEW [Fatal]: Always use Early Return. (Leave Success return to LAST)
            return new MessageResponse()
            {
                Msg = "   تم تجديدالخدمة بداية من تاريخ اليوم: " + " بنجاح! ",
            };
        }
        //TODO: REVIEW [Warning]: Variable Naming
        var newdateNow = datetimenow.AddDays(30);
        datetimenow = newdateNow;
        var duration = datetimenow.CompareTo(data.EndDate);
        if (datetimenow >= data.EndDate)
        {
            var newEndDate = data.EndDate.AddYears(1);
            data.EndDate = newEndDate;
            await _dbContext.SaveChangesAsync();
            return new MessageResponse()
            {
                Msg = data.EndDate.ToString() + "   تم تجديدالخدمة بداية من تاريخ الانتهاء للخدمة: " + " بنجاح! ",
            };
        }
        //TODO: REVIEW [Warning]: Messages need to be corrected
        throw new BadRequestException("   عذرا لم يتم تجديد الخدمة يمكنك التجديد عند انتهاء الخدمة او قبلها ب 30 يوما ");
    }
    
    public async Task<MessageResponse> UploadFile(int id, FileDto file)
    {
        //TODO: REVIEW [Error]: Change Model to allow Multiple Files
        //TODO: REVIEW [Warning]: Upon making changes to Model, you no longer need to fetch the Subscription
        // Use require only only check it exists!
        var data = await _dbContext.Subscriptions.SingleOrDefaultAsync(a => a.Id == id);
        if (data == null) throw new BadRequestException("thear is no subscription with this number");
        var fileType = "." + file.File.FileName.Split('.')[file.File.FileName.Split('.').Length - 1];
        var fileName = id + "_" + DateTime.UtcNow.Ticks + file.File.FileName;
        //TODO: REVIEW [Recommendation]: Create a service responsible for File Uploads to make it managable
        var pathBuild = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files");
        if (!Directory.Exists(pathBuild))
        {
            Directory.CreateDirectory(pathBuild);
        }
        var path = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files\\" + fileName);
        await using (var stream = new FileStream(path, FileMode.Create))
        {
            await file.File.CopyToAsync(stream);
        }
        var newFile = new SubscriptionFile()
        {
            //TODO: REVIEW [Warning]: CreatedById should be the current user (After Identity Completed)
            CreatedById = 3,
            CreatedOn = DateTime.UtcNow,
            FileName = path,
            FileType = fileType,
            SubscriptionId = id,
        };
        await _dbContext.SubscriptionFiles.AddAsync(newFile);
        await _dbContext.SaveChangesAsync();
        data.SubscriptionFileId = newFile.Id;
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
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
        //TODO: REVIEW [Fatal]: Count will only return the requested page
        var totalCount = queryResult.Count();
        var totalpages = (int)Math.Ceiling(totalCount / (decimal)request.PageSize);
        //TODO: REVIEW [Fatal]: Return List of Objects containing: FileUrl, Date Added, Document Type
        return new FetchSubscriptionFileResponseDto()
        {
            Content = result,
            CurrentPage = request.PageNumber,
            PageSize = request.PageSize,
            TotalPages = totalpages,
        };
    }

    public async Task<SubscriptionFileResponsDto> GetFiles(int id)
    {
        //TODO: REVIEW [Error]: Return List of SubscriptionFiles Based on SubscriptionId
        var data = await _dbContext.Subscriptions.SingleOrDefaultAsync(a => a.Id == id);
        var fileId = data.SubscriptionFileId;
        var fileData = await _dbContext.SubscriptionFiles.SingleOrDefaultAsync(a => a.Id == fileId);
        return new SubscriptionFileResponsDto() { Id = fileData.Id, FileName = fileData.FileName , FileType = fileData.FileType };
    }
    
    public async Task<MessageResponse> Lock(int id)
    {
        //TODO: REVIEW [Error]: Match against Not deleted
        var data = await _dbContext.Subscriptions.FirstOrDefaultAsync(b => b.Id == id && b.Status == GeneralStatus.Active);
        if (data == null)
        {
            //TODO: REVIEW [Error]: For Clearer intent use previous Query & then match again Locked Outide null
            var isLocked = await _dbContext.Subscriptions.Where(b => b.Id == id && b.Status == GeneralStatus.LockedByUser).AnyAsync();
            if (!isLocked)
            {
                throw new BadRequestException("thear is no subscription with this number");
            }
            throw new NotFoundException("عفوًا الاشتراك مقفل مسبقًا !");
        }
        data.Status = GeneralStatus.LockedByUser;
        await _dbContext.SaveChangesAsync();
        //TODO: REVIEW [Fatal]: Using Customer without Include will cause an exception
        return new MessageResponse()
        {
            Msg = data.Customer.Name + "  لقد تم قفل الاشتراك لهاذا العميل: " + " بنجاح! ",
        };
    }
    public async Task<MessageResponse> Unlock(int id)
    {
        //TODO: REVIEW [Fatal][Error]: Use Previous Review Notes
        var data = await _dbContext.Subscriptions.FirstOrDefaultAsync(b => b.Id == id && b.Status == GeneralStatus.LockedByUser);
        if (data == null)
        {
            var isUnLocked = await _dbContext.Subscriptions.Where(b => b.Id == id && b.Status == GeneralStatus.Active).AnyAsync();
            if (!isUnLocked)
            {
                throw new BadRequestException("thear is no subscription with this number");
            }
            throw new NotFoundException("عفوًا الاشتراك غير مقفل مسبقًا !");
        }
        data.Status = GeneralStatus.Active;
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = data.Customer.Name + " تم فتح القفل عن الاشتراك: " + " بنجاح! ",
        };
    }
    public async Task<MessageResponse> Remove(int id)
    {
        var data = await _dbContext.Subscriptions.FirstOrDefaultAsync(a => a.Id == id && a.Status != GeneralStatus.Deleted);
        if (data == null) throw new NotFoundException("thear is no subscription with this id ");
        if (data.Status == GeneralStatus.LockedByUser)
            throw new BadRequestException("this subscription is locked by user you cannot Removed");
        data.Status = GeneralStatus.Deleted;
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "subscription is removed"
        };
    }
}
 

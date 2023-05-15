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
using System.Net.Http;
using Microsoft.VisualBasic;
using Infrastructure.Constants;
using Shared.Exceptions;
using ManagementAPI.Dtos.Companion;
using AutoMapper.QueryableExtensions;
using Azure;
using System.Web;


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

    public async Task<MessageResponse> Create( FileDto fileRequest,CreateSubscriptionDto request)
    {
        var data = _mapper.Map<Subscription>(request);
        await _dbContext.Subscriptions.AddAsync(data);
        await _dbContext.SaveChangesAsync();
        await UploadFile(data.Id,fileRequest);
        return new MessageResponse()
        {
            Msg = "ok subscription created"
        };
    }

    public async Task<FetchSubscriptionResponseDto> GetAll(FetchSubscriptionRequestDto request)
    {
        var data = await _dbContext.Subscriptions
            .Where(p => p.Status != GeneralStatus.Deleted)
            .Include(a => a.Service)
            .Include(a => a.Customer)
            .OrderBy(p => p.Id)
            .Skip(request.PageSize * (request.PageNumber - 1))
            .Take(request.PageSize)
            .ProjectTo<SubscriptionRsponseDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        /* var result = _mapper.Map<List<SubscriptionRsponseDto>>(Subs_Query);*/
        var query = _dbContext.Subscriptions
            .Where(p => p.Status != GeneralStatus.Deleted);
        var totalCount = await query.CountAsync();
        var totalpages = (int)Math.Ceiling(totalCount / 25.00);
        return new FetchSubscriptionResponseDto()
        {
            Content = data,
            CurrentPage = request.PageNumber,
            PageSize = request.PageSize,
            TotalPages = totalpages,
        };

    }
    public async Task<MessageResponse> Renew(int id)
    {
        if (id < 0) throw new BadRequestException("! incorrect  id ");
        var data = await _dbContext.Subscriptions.SingleOrDefaultAsync(a => a.Id == id && a.Status != GeneralStatus.Deleted);
        if (data == null) throw new NotFoundException("no subscription with this number");
        if (data.Status == GeneralStatus.LockedByUser) throw new BadRequestException("subsciption is locked by user you cannot renew ");
        //TODO: REVIEW [Warning]: Variable Naming
        var dateTimeNow = DateTime.UtcNow;
        //TODO: REVIEW [Fatal]: Logic flow is too broad, Use Function instead with Clear Name
        /*var isExpired = dateTimeNow.CompareTo(data.EndDate);*/
        if (dateTimeNow > data.EndDate)
        {
            data.EndDate = dateTimeNow.AddYears(1);
            await _dbContext.SaveChangesAsync();
            //TODO: REVIEW [Fatal]: Always use Early Return. (Leave Success return to LAST)
            return new MessageResponse()
            {
                Msg = "   تم تجديدالخدمة بداية من تاريخ اليوم: " + " بنجاح! ",
            };
        }
        dateTimeNow = dateTimeNow.AddDays(30);
        /*var duration = datetimenow.CompareTo(data.EndDate);*/
        if (dateTimeNow >= data.EndDate)
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
        throw new BadRequestException("   عذرا لم يتم تجديد الخدمة يمكنك التجديد عند انتهاء تاريخ الخدمة او قبلها ب 30 يوما ");
    }
    
    public async Task<MessageResponse> UploadFile(int id, FileDto file)
    {

        //TODO: REVIEW [Error]: Change Model to allow Multiple Files
        //TODO: REVIEW [Warning]: Upon making changes to Model, you no longer need to fetch the Subscription
        // Use require only only check it exists!
        if (id < 0) throw new BadRequestException("! incorrect  id ");
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
            CreatedOn = DateTime.UtcNow,
            FileName = path,
            FileType = fileType,
            SubscriptionId = id,
            CreatedById = 3,
            Status = GeneralStatus.Active
        };
        await _dbContext.SubscriptionFiles.AddAsync(newFile);
        await _dbContext.SaveChangesAsync();
        /*data.SubscriptionFileId = newFile.Id;*/
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "file uploaded"
        };
    }

    public async Task<FetchSubscriptionFileResponseDto> GetFiles(FetchSubscriptionRequestDto request)
    {
        var queryResult = await _dbContext.SubscriptionFiles
            .Where(p => p.Status != GeneralStatus.Deleted)
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
    public async Task<string> GetPageContent(string url)
    {
        using (var client = new HttpClient())
        {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
    }
    public async Task<List<SubscriptionFileResponsDto>> GetFileById(int id)
    {
        if (id < 0) throw new BadRequestException("! incorrect  id ");
        var data = await _dbContext.SubscriptionFiles.Where(a => a.SubscriptionId == id && a.Status != GeneralStatus.Deleted).
            ProjectTo<SubscriptionFileResponsDto>(_mapper.ConfigurationProvider).ToListAsync();
        if (data == null) throw new BadRequestException("no file with this subs number");
        return data;
    }
    
    public async Task<MessageResponse> Lock(int id)
    {
        if (id < 0) throw new BadRequestException("! incorrect  id ");
        var data = await _dbContext.Subscriptions.FirstOrDefaultAsync(b => b.Id == id && b.Status != GeneralStatus.Deleted);
        if (data == null)
        {
            throw new NotFoundException("thear is no subscription with this number");
          
        }
        if (data.Status == GeneralStatus.LockedByUser)
        {
            throw new BadRequestException("عفوًا الاشتراك مقفل مسبقًا !");
        }
        data.Status = GeneralStatus.LockedByUser;
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg =  "  لقد تم قفل الاشتراك لهاذا العميل: " + " بنجاح! ",
        };
    }
    public async Task<MessageResponse> Unlock(int id)
    {
        if (id < 0) throw new BadRequestException("! incorrect  id ");
        var data = await _dbContext.Subscriptions.FirstOrDefaultAsync(b => b.Id == id && b.Status != GeneralStatus.Deleted);
        if (data == null)
        {
            throw new NotFoundException("thear is no subscription with this number");

        }
        if (data.Status == GeneralStatus.Active)
        {
            throw new BadRequestException("عفوًا الاشتراك غير مقفل مسبقًا !");
        }
        data.Status = GeneralStatus.Active;
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "  لقد تم فك قفل الاشتراك لهاذا العميل: " + " بنجاح! ",
        };
    }
    public async Task<MessageResponse> Remove(int id)
    {
        if (id < 0) throw new BadRequestException("! incorrect  id ");
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
    public async Task<MessageResponse> RemoveFile(int id)
    {
        if (id < 0) throw new BadRequestException("! incorrect  id ");
        var data = await _dbContext.SubscriptionFiles.FirstOrDefaultAsync(a => a.Id == id && a.Status != GeneralStatus.Deleted);
        if (data == null) throw new NotFoundException("thear is no subscription file with this id ");
        if (data.Status == GeneralStatus.LockedByUser)
            throw new BadRequestException("this subscription file is locked by user you cannot Removed");
        data.Status = GeneralStatus.Deleted;
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "subscription file is removed"
        };
    }
}
 

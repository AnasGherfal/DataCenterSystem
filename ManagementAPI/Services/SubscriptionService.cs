

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Azure.Core;
using Infrastructure;
using Infrastructure.Constants;
using Infrastructure.Models;
using ManagementAPI.Dtos;
using ManagementAPI.Dtos.Invoice;
using ManagementAPI.Dtos.Service;
using ManagementAPI.Dtos.Subscriptions;
using Microsoft.EntityFrameworkCore;
using Shared.Constants;
using Shared.Dtos;
using Shared.Exceptions;

namespace ManagementAPI.Services;

//TODO: REVIEW [Consistency]: Use Interface For DI
public class SubscriptionService:ISubscriptionService
{
    //TODO: REVIEW [Warning]: Response Messages need to be reviewed
    private readonly DataCenterContext _dbContext;
    private readonly IUploadFileService _uploadFile;
    private readonly IMapper _mapper;
    public SubscriptionService(DataCenterContext dbContext, IMapper mapper, IUploadFileService uploadFile)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _uploadFile = uploadFile;
    }

    public async Task<MessageResponse> Create(CreateSubscriptionRequestDto request)
    {
        var data = _mapper.Map<Subscription>(request) ?? throw new BadRequestException("! طلبك غير صالح يرجى إعادة المحاولة");
        _dbContext.Subscriptions.Add(data);
        _dbContext.SaveChanges();
        await _uploadFile.Upload(request.File,EntityType.SubscriptionFile,data);
        return new MessageResponse()
        {
            Msg = "تم اضافة الإشتراك بنجاح!"
        };
    }
    public async Task<MessageResponse> Update(Guid id , UpdateSubscriptionRequestDto request)
    {
       
        var query = await _dbContext.Subscriptions.SingleOrDefaultAsync(x => x.Id == id && x.Status != GeneralStatus.Deleted) ?? throw new BadRequestException("thear is no subscription with this number");
        if (query.Status == GeneralStatus.LockedByUser) throw new BadRequestException("عذرًا...يبدو أن هذا الإشتراك قد تم تقييده يرجى مراجعة المسؤول ! ");
        var data = _mapper.Map<Subscription>(request);
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "تم تعديل هذا الإشتراك بنجاح!"
        };

    }

    public async Task<FetchSubscriptionResponseDto> GetAll(FetchSubscriptionRequestDto request)
    {
        var query = _dbContext.Subscriptions
            .Include(a => a.Service)
            .Include(a => a.Customer)
            .Where(p => p.Status != GeneralStatus.Deleted);
            var data= await query.OrderBy(p => p.Id)
            .Skip(request.PageSize * (request.PageNumber - 1))
            .Take(request.PageSize)
            .ProjectTo<SubscriptionRsponseDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        var totalCount = query.Count();
        var totalpages = (int)Math.Ceiling(totalCount /(decimal)request.PageSize);
        return new FetchSubscriptionResponseDto()
        {
            Content = data,
            CurrentPage = request.PageNumber,
            TotalPages = totalpages,
        };

    }
    public async Task<MessageResponse> Renew(Guid id)
    {
        var data = await _dbContext.Subscriptions.SingleOrDefaultAsync(a => a.Id == id && a.Status != GeneralStatus.Deleted) ?? throw new NotFoundException("عذرًا لا وجود لإشتراك مفعل بهذا الرقم");
        if (data.Status == GeneralStatus.LockedByUser) throw new BadRequestException("عذرًا...يبدو أن هذا الإشتراك قد تم تقييده يرجى مراجعة المسؤول ! ");
        var duration = DateTime.Now.AddDays(29);
        if (!IsExpired(data))
            if (duration > data.EndDate)
                throw new BadRequestException("عذرًا لا يمكنك تجديد هذا الإشتراك! التجديد يتم عند انتهاء الإشتراك أو قبل إنتهاءه بمدة 30 يومًا! ");
        //TODO:["Review | Qustion"]Is started With new StartDate Or Create NEw Subscription???
        data.EndDate = data.EndDate.AddYears(1);
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "لقد تم تجديد هذا الإشتراك بنجاح !"
        };
    }
    
   /* public async Task<MessageResponse> UploadFile(int id, IFormFile file)
    {

        //TODO: REVIEW [Error]: Change Model to allow Multiple Files
        //TODO: REVIEW [Warning]: Upon making changes to Model, you no longer need to fetch the Subscription
        // Use require only only check it exists!
        if (id < 0) throw new BadRequestException("! incorrect  id ");
        var data = await _dbContext.Subscriptions.SingleOrDefaultAsync(a => a.Id == id) ?? throw new BadRequestException("thear is no subscription with this number");
        var fileType = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
        var fileName = id + "_" + DateTime.UtcNow.Ticks + file.FileName;
        //TODO: REVIEW [Recommendation]: Create a service responsible for File Uploads to make it managable
        var pathBuild = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files");
        if (!Directory.Exists(pathBuild))
        {
            Directory.CreateDirectory(pathBuild);
        }
        var path = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files\\" + fileName);
        await using (var stream = new FileStream(path, FileMode.Create))
        {
            await file.CopyToAsync(stream);
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
        *//*data.SubscriptionFileId = newFile.Id;*//*
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "file uploaded"
        };
    }*/

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
        var totalCount = queryResult.Count;
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
    public async Task<MessageResponse> UpdateFile(Guid id , FileRequestDto request)
    {
       
        var query = await _dbContext.SubscriptionFiles.Where(a => a.SubscriptionId == id && a.Status != GeneralStatus.Deleted).Include(p=>p.Subscription).ToListAsync() ?? throw new NotFoundException("عذرًا لا وجود لملف لهذا الإشتراك..يرجى مراجعة الطلب!");
        foreach ( var item in query)
            item.Status = GeneralStatus.Deleted;
        await _dbContext.SaveChangesAsync();
        await _uploadFile.Upload(request, EntityType.SubscriptionFile, query.Select(p => p.Subscription).Single());
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "تم تحديث الملف الخاص بهذا الإشتراك بنجاح!"
        };

        

    }
    public async Task<string> GetPageContent(string url)
    {
        using var client = new HttpClient();
        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return content;
    }
    public async Task<List<SubscriptionFileResponsDto>> GetFileById(Guid id)
    {
       
        var data = await _dbContext.SubscriptionFiles.Where(a => a.SubscriptionId == id && a.Status != GeneralStatus.Deleted).
            ProjectTo<SubscriptionFileResponsDto>(_mapper.ConfigurationProvider).ToListAsync() ?? throw new BadRequestException("no file with this subs number");
        return data;
    }
    public async Task<FileStream> Download(Guid id)
    {
        var data = await  _dbContext.SubscriptionFiles.SingleOrDefaultAsync(a => a.SubscriptionId == id && a.Status != GeneralStatus.Deleted) ?? throw new BadRequestException("no file with this subs number");
        var path = data.FileName;
        // Check if the file exists.
        if (!File.Exists(path))
        {
            throw new FileNotFoundException("File not found: ");
        }

        // Open the file for reading.
        return File.OpenRead(path);
    }

    public async Task<MessageResponse> Lock(Guid id)
    {
        
        var data = await _dbContext.Subscriptions.FirstOrDefaultAsync(b => b.Id == id && b.Status != GeneralStatus.Deleted) ?? throw new NotFoundException("thear is no subscription with this number");
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
    public async Task<MessageResponse> Unlock(Guid id)
    {
        var data = await _dbContext.Subscriptions.FirstOrDefaultAsync(b => b.Id == id && b.Status != GeneralStatus.Deleted) ?? throw new NotFoundException("thear is no subscription with this number");
        if (IsLocked(data.Status))
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
    public async Task<MessageResponse> Delete(Guid id)
    {
        var data = await _dbContext.Subscriptions.FirstOrDefaultAsync(a => a.Id == id && a.Status != GeneralStatus.Deleted) ?? throw new NotFoundException("thear is no subscription with this id ");
        if (IsLocked(data.Status))
            throw new BadRequestException("this subscription is locked by user you cannot Removed");
        data.Status = GeneralStatus.Deleted;
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "subscription is removed"
        };
    }
    public async Task<MessageResponse> DeleteFile(Guid id)
    {
        var data = await _dbContext.SubscriptionFiles.FirstOrDefaultAsync(a => a.Id == id && a.Status != GeneralStatus.Deleted) ?? throw new NotFoundException("thear is no subscription file with this id ");
        if (IsLocked(data.Status))
            throw new BadRequestException("this subscription file is locked by user you cannot Removed");
        data.Status = GeneralStatus.Deleted;
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "subscription file is removed"
        };
    }
    public async Task<SubscriptionRsponseDto> GetById(Guid id)
    {
       
        var data = await _dbContext.Subscriptions.Where(p => p.Id == id && p.Status != GeneralStatus.Deleted)
            .Include(p => p.Visits)
            .Include(a => a.Service)
            .Include(a => a.Customer).SingleOrDefaultAsync() ?? throw new BadHttpRequestException("عذرًا لا وجود لإشتراك بهذا الرقم");
        
        var result = _mapper.Map<SubscriptionRsponseDto>(data);
        return result;
    }

    private static bool IsExpired(Subscription subscription)
    {
        var nowDate=DateTime.UtcNow;
        if(subscription.EndDate<nowDate)
            return true;
        else
            return false;
    }
    private static bool IsLocked(GeneralStatus status)
    {
        return status switch
        {
            GeneralStatus.Active => false,
            GeneralStatus.LockedByUser => true,
            _ => true,
        };
    }
}
 

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Infrastructure;
using Infrastructure.Constants;
using Infrastructure.Models;
using ManagementAPI.Dtos.Subscriptions;
using Microsoft.EntityFrameworkCore;
using Shared.Constants;
using Shared.Dtos;
using Shared.Exceptions;
using Microsoft.AspNetCore.StaticFiles;
using System.Threading;
using System.Net.Http.Headers;

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
        var service = _dbContext.Services.Where(p => p.Id == data.ServiceId).SingleOrDefault()?? throw new BadRequestException("هنالك مشكلة في الباقة..يرجى مراجعة الدعم الفني");
        var customer = _dbContext.Customers.Where(p => p.Id == data.CustomerId).SingleOrDefault() ?? throw new BadRequestException("هنالك مشكلة في حساب الزبون..يرجى مراجعة الدعم الفني");
        data.TotalPrice = service.Price;
        data.MonthlyVisits = service.MonthlyVisits;
        data.Customer=customer;
        _dbContext.Subscriptions.Add(data);
        _dbContext.SaveChanges();
        var subFile = new FileRequestDto() { File = request.File, DocType = 4 };
        await _uploadFile.Upload(subFile,EntityType.SubscriptionFile,data);
        return new MessageResponse()
        {
            Msg = "تم اضافة الإشتراك بنجاح!"
        };
    }
    public async Task<MessageResponse> Update(Guid id , UpdateSubscriptionRequestDto request)
    {
       
        var data = await _dbContext.Subscriptions.
            Include(p => p.SubscriptionFile)
            .SingleOrDefaultAsync(x => x.Id == id && x.Status != GeneralStatus.Deleted)
            ?? throw new BadRequestException("عذرًا لايوجد إشتراك بهذا الرقم..");
        if (data.Status == GeneralStatus.Locked) throw new BadRequestException("عذرًا...يبدو أن هذا الإشتراك قد تم تقييده يرجى مراجعة المسؤول ! ");
       
        if (request.File != null) {
            data.SubscriptionFile.IsActive = GeneralStatus.Deleted;
            await _uploadFile.Upload(request.File, EntityType.SubscriptionFile, data);
                }
        _mapper.Map(data, request);
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "تم تعديل هذا الإشتراك بنجاح!"
        };

    }

    public async Task<FetchSubscriptionResponseDto> GetAll(FetchSubscriptionRequestDto request)
    {
        var query = _dbContext.Subscriptions.Where(p=> p.Status!=GeneralStatus.Deleted);
        IQueryable<Subscription> data;
        var timeLeft = DateTime.UtcNow.AddDays(30);
        switch (request.Status)
        {
            case Status.Active:
                data = query.Where(p =>  p.EndDate  > DateTime.UtcNow);
                break;
            case Status.AboutToExpired:
               data = query.Where(p => p.EndDate <= timeLeft && DateTime.UtcNow < p.EndDate);
                break;
            case Status.Expired:
                data = query.Where(p =>p.EndDate <=DateTime.UtcNow);
                break;
        }

        var result =await data
            .OrderBy(p => p.StartDate)
            .ProjectTo<SubscriptionRsponseDto>(_mapper.ConfigurationProvider)
            .Skip(request.PageSize * (request.PageNumber - 1))
            .Take(request.PageSize)
            .ToListAsync();
        var totalCount = query.Count();
        var totalpages = (int)Math.Ceiling(totalCount /(decimal)request.PageSize);
        return new FetchSubscriptionResponseDto()
        {
            Content =result,
            CurrentPage = request.PageNumber,
            TotalPages = totalpages,
        };

    }
    public async Task<FetchSubscriptionFilterResponseDto> SubscriptionsFilter()
    {
        var query = await _dbContext.Subscriptions.Include(p => p.Customer).Include(p => p.SubscriptionFile)
                                               .Include(p => p.Visits).Include(p => p.Service).Where(p => p.Status != GeneralStatus.Deleted)
                                               .ProjectTo<SubscriptionRsponseDto>(_mapper.ConfigurationProvider).ToListAsync();
        var activeSubscriptions =query.Where(p => p.DaysRemaining > 30).ToList();
        var aboutToExpierdSubs = query.Where(p => p.DaysRemaining <= 30 && p.DaysRemaining >0).ToList();
        var expiredSubs = query.Where(p => p.DaysRemaining <= 0).ToList();
        return new FetchSubscriptionFilterResponseDto()
        {
            FilteredContent = new List<FilterSubscriptionResponseDto>()
          {
              new FilterSubscriptionResponseDto()
              {
                  Count= activeSubscriptions.Count(),
                  Status=Status.Active
              },
              new FilterSubscriptionResponseDto()
              {
                  Count= aboutToExpierdSubs.Count(),
                  Status=Status.AboutToExpired
              },
             new FilterSubscriptionResponseDto()
             {
                 Count = expiredSubs.Count(),
                 Status=Status.Expired

             }
          }
        };
    }
    public async Task<MessageResponse> Renew(FileRequestDto file, Guid id)
    {
        var data = await _dbContext.Subscriptions.Include(p => p.SubscriptionFile)
                                                 .SingleOrDefaultAsync(a => a.Id == id && a.Status != GeneralStatus.Deleted) 
                                                 ?? throw new NotFoundException("عذرًا لا وجود لإشتراك مفعل بهذا الرقم");
        if (data.Status == GeneralStatus.Locked) throw new BadRequestException("عذرًا...يبدو أن هذا الإشتراك قد تم تقييده يرجى مراجعة المسؤول ! ");
        var duration = DateTime.Now.AddDays(29);
         if (!IsExpired(data))
             if (duration > data.EndDate)
                 throw new BadRequestException("عذرًا لا يمكنك تجديد هذا الإشتراك! التجديد يتم عند انتهاء الإشتراك أو قبل إنتهاءه بمدة 30 يومًا! ");
        Subscription reNewSubcription = new()
        {
            StartDate = DateTime.UtcNow,
            EndDate = data.EndDate.AddYears(1),
            Status = GeneralStatus.Active,
            AdditionalPowers = data.AdditionalPowers,
            CustomerId = data.CustomerId,
            TotalPrice = data.TotalPrice,
            ServiceId = data.ServiceId,
            Visits = data.Visits,
            CreatedById = data.CreatedById,
            CreatedOn = DateTime.UtcNow,
            Invoices = data.Invoices

        };
        data.Status = GeneralStatus.Deleted;
        data.SubscriptionFile.IsActive = GeneralStatus.Deleted;
        _dbContext.Subscriptions.Add(reNewSubcription);
        await _dbContext.SaveChangesAsync();
        await _uploadFile.Upload(file, EntityType.SubscriptionFile, reNewSubcription);
        return new MessageResponse()
        {
            Msg = "لقد تم تجديد هذا الإشتراك بنجاح !"
        };
    }

    public async Task<FileStreamResult> Download(Guid Id)
    {
        var data = await  _dbContext.SubscriptionFiles.SingleOrDefaultAsync(a => a.SubscriptionId == Id && a.IsActive != GeneralStatus.Deleted) ?? throw new BadRequestException("no file with this subs number");
        var path = data.FilePath;
        // Check if the file exists.
     
        if (data.IsActive != GeneralStatus.Active) throw new NotFoundException("FILE_NOT_ACTIVE");
        if (!File.Exists(data.FilePath)) throw new NotFoundException("File not found: ");
        var fileContents = await File.ReadAllBytesAsync(data.FilePath);
        var stream = new MemoryStream(fileContents);
        var contentType = new FileExtensionContentTypeProvider();
        if(!contentType.TryGetContentType(data.FilePath, out var fileType))
        {
            fileType = "application/octet-stream";
        }
        return new FileStreamResult(stream, new MediaTypeHeaderValue(fileType).ToString())
        {
            FileDownloadName = Path.GetFileName(data.FilePath)
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

    public async Task<MessageResponse> Lock(Guid id)
    {
        
        var data = await _dbContext.Subscriptions.Include(p=>p.SubscriptionFile).FirstOrDefaultAsync(b => b.Id == id && b.Status != GeneralStatus.Deleted) ?? throw new NotFoundException("thear is no subscription with this number");
        if (data.Status == GeneralStatus.Locked)
        {
            throw new BadRequestException("عفوًا الاشتراك مقفل مسبقًا !");
        }
        data.Status = GeneralStatus.Locked;
        data.SubscriptionFile.IsActive = GeneralStatus.Deleted;
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg =  "  لقد تم قفل الاشتراك لهاذا العميل: " + " بنجاح! ",
        };
    }
    public async Task<MessageResponse> Unlock(Guid id)
    {
        var data = await _dbContext.Subscriptions.Include(x=>x.SubscriptionFile).FirstOrDefaultAsync(b => b.Id == id && b.Status != GeneralStatus.Deleted) ?? throw new NotFoundException("thear is no subscription with this number");
        if (!IsLocked(data.Status))
        {
            throw new BadRequestException("عفوًا الاشتراك غير مقفل مسبقًا !");
        }
        data.Status = GeneralStatus.Active;
        data.SubscriptionFile.IsActive = GeneralStatus.Active;
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "  لقد تم فك قفل الاشتراك لهاذا العميل: " + " بنجاح! ",
        };
    }
    public async Task<MessageResponse> Delete(Guid id)
    {
        var data = await _dbContext.Subscriptions.Include(x=>x.SubscriptionFile).Where(p=>p.SubscriptionFile.IsActive!=GeneralStatus.Deleted).FirstOrDefaultAsync(a => a.Id == id && a.Status != GeneralStatus.Deleted) ?? throw new NotFoundException("thear is no subscription with this id ");
        if (IsLocked(data.Status))
            throw new BadRequestException("عذرًا هذا الإشتراك مقيد !");
        data.Status = GeneralStatus.Deleted;
        data.SubscriptionFile.IsActive = GeneralStatus.Deleted;
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "تم حذف هذا الإشتراك بنجاح!"
        };
    }
    public async Task<SubscriptionRsponseDto> GetById(Guid id)
    {
       
        var data = await _dbContext.Subscriptions.Where(p => p.Id == id && p.Status != GeneralStatus.Deleted)
            .Include(p => p.Visits)
            .Include(a => a.Service)
            .Include(b => b.SubscriptionFile)
            .Include(a => a.Customer)
            .ProjectTo<SubscriptionRsponseDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync() ?? throw new BadHttpRequestException("عذرًا لا وجود لإشتراك بهذا الرقم");

        return data;
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
            GeneralStatus.Locked => true,
            _ => true,
        };
    }
}
 

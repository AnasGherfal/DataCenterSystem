using AutoMapper;
using AutoMapper.QueryableExtensions;
using Azure.Core;
using Infrastructure;
using Infrastructure.Constants;
using Infrastructure.Models;
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
        var service = _dbContext.Services.Where(p => p.Id == data.ServiceId).SingleOrDefault()?? throw new BadRequestException("هنالك مشكلة في الباقة..يرجى مراجعة الدعم الفني");
        data.TotalPrice = service.Price;
        data.MonthlyVisits = service.MonthlyVisits;
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
       
        var data = await _dbContext.Subscriptions.
            Include(p => p.SubscriptionFile)
            .SingleOrDefaultAsync(x => x.Id == id && x.Status != GeneralStatus.Deleted)
            ?? throw new BadRequestException("عذرًا لايوجد إشتراك بهذا الرقم..");
        if (data.Status == GeneralStatus.Locked) throw new BadRequestException("عذرًا...يبدو أن هذا الإشتراك قد تم تقييده يرجى مراجعة المسؤول ! ");
        _mapper.Map(data, request);
        if (request.File != null) {
            data.SubscriptionFile.IsActive = (short)GeneralStatus.Deleted;
            await _uploadFile.Upload(request.File, EntityType.SubscriptionFile, data);
                }
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "تم تعديل هذا الإشتراك بنجاح!"
        };

    }

    public async Task<FetchSubscriptionResponseDto> GetAll(FetchSubscriptionRequestDto request)
    {
        var query = _dbContext.Subscriptions.Include(p => p.Customer).Include(p => p.SubscriptionFile)
                                                .Include(p => p.Visits).Include(p => p.Service).Where(p => p.Status != GeneralStatus.Deleted)
                                                .ProjectTo<SubscriptionRsponseDto>(_mapper.ConfigurationProvider);
            var data= await query.OrderBy(p => p.StartDate)
            .Skip(request.PageSize * (request.PageNumber - 1))
            .Take(request.PageSize)
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
    public async Task<MessageResponse> Renew(FileRequestDto file, Guid id)
    {
        var data = await _dbContext.Subscriptions.Include(p => p.SubscriptionFile).SingleOrDefaultAsync(a => a.Id == id && a.Status != GeneralStatus.Deleted) ?? throw new NotFoundException("عذرًا لا وجود لإشتراك مفعل بهذا الرقم");
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
        data.SubscriptionFile.IsActive = (short)GeneralStatus.Deleted;
        _dbContext.Subscriptions.Add(reNewSubcription);
        await _dbContext.SaveChangesAsync();
        await _uploadFile.Upload(file, EntityType.SubscriptionFile, reNewSubcription);
        return new MessageResponse()
        {
            Msg = "لقد تم تجديد هذا الإشتراك بنجاح !"
        };
    }

    public async Task<FileStream> Download(Guid id)
    {
        var data = await  _dbContext.SubscriptionFiles.SingleOrDefaultAsync(a => a.SubscriptionId == id && a.IsActive == (short)GeneralStatus.Active) ?? throw new BadRequestException("no file with this subs number");
        var path = data.FilePath;
        // Check if the file exists.
        if (!File.Exists(path))
        {
            throw new FileNotFoundException("عذرًا لا وجود لملف.. ");
        }

        // Open the file for reading.
        return File.OpenRead(path);
    }

    public async Task<MessageResponse> Lock(Guid id)
    {
        
        var data = await _dbContext.Subscriptions.Include(p=>p.SubscriptionFile).FirstOrDefaultAsync(b => b.Id == id && b.Status != GeneralStatus.Deleted) ?? throw new NotFoundException("thear is no subscription with this number");
        if (data.Status == GeneralStatus.Locked)
        {
            throw new BadRequestException("عفوًا الاشتراك مقفل مسبقًا !");
        }
        data.Status = GeneralStatus.Locked;
        data.SubscriptionFile.IsActive = (short)GeneralStatus.Deleted;
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg =  "  لقد تم قفل الاشتراك لهاذا العميل: " + " بنجاح! ",
        };
    }
    public async Task<MessageResponse> Unlock(Guid id)
    {
        var data = await _dbContext.Subscriptions.Include(x=>x.SubscriptionFile).FirstOrDefaultAsync(b => b.Id == id &&b.SubscriptionFile.IsActive!= (short)GeneralStatus.Deleted && b.Status != GeneralStatus.Deleted) ?? throw new NotFoundException("thear is no subscription with this number");
        if (IsLocked(data.Status))
        {
            throw new BadRequestException("عفوًا الاشتراك غير مقفل مسبقًا !");
        }
        data.Status = GeneralStatus.Active;
        data.SubscriptionFile.IsActive = (short) GeneralStatus.Active;
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "  لقد تم فك قفل الاشتراك لهاذا العميل: " + " بنجاح! ",
        };
    }
    public async Task<MessageResponse> Delete(Guid id)
    {
        var data = await _dbContext.Subscriptions.Include(x=>x.SubscriptionFile).FirstOrDefaultAsync(a => a.Id == id && a.SubscriptionFile.IsActive!=(short)GeneralStatus.Deleted && a.Status != GeneralStatus.Deleted) ?? throw new NotFoundException("thear is no subscription with this id ");
        if (IsLocked(data.Status))
            throw new BadRequestException("عذرًا هذا الإشتراك مقيد !");
        data.Status = GeneralStatus.Deleted;
        data.SubscriptionFile.IsActive = (short)GeneralStatus.Deleted;
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
 

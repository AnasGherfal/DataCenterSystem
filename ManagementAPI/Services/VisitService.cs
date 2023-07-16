

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infrastructure;
using Infrastructure.Constants;
using Infrastructure.Models;
using ManagementAPI.Dtos.Visit;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Exceptions;

namespace ManagementAPI.Services;

public class VisitService : IVisitService
{
    private readonly IMapper _mapper;
    private readonly DataCenterContext _dbContext;
    public VisitService(IMapper mapper, DataCenterContext dbContext)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<MessageResponse> Create(CreateVisitRequestDto request)
    {
        var timeShifts = await _dbContext.VisitTimeShifts.Where(p => p.Status == GeneralStatus.Active).ToListAsync();
        var subscription = await _dbContext.Subscriptions.Where(p => p.Id == request.SubscriptionId && p.Status == GeneralStatus.Active).Include(p => p.Customer).ThenInclude(p=>p.Representatives).SingleOrDefaultAsync() ?? throw new BadRequestException("عذرًا يرجى التأكد من الإشتراك الخاص بالزبون!");
        
        var visitStartTime = request.StartTime.TimeOfDay;
        var visitEndTime = request.EndTime.TimeOfDay;
        foreach (var shift in timeShifts)
        {
            if (visitStartTime >= shift.StartTime && visitStartTime <= shift.EndTime)
            {

                if (visitEndTime >= shift.EndTime)
                {
                    var totalTime = request.EndTime - request.StartTime;
                    var timeInThisShift = shift.EndTime - visitStartTime;
                    var firstEndTime = TimeOnly.FromTimeSpan(shift.EndTime);
                    var lastEndTime = TimeOnly.FromTimeSpan(request.EndTime.TimeOfDay);
                    var timeInAnotherShift = totalTime - timeInThisShift;
                    var anotherTimeShift = timeShifts.Where(p => p.EndTime >= lastEndTime.ToTimeSpan()).Single();
                    var newVisit = new CreateVisitRequestDto()
                    {
                        Companions = request.Companions,
                        Representatives = request.Representatives,
                        StartTime = DateOnly.FromDateTime(request.StartTime).ToDateTime(firstEndTime),
                        EndTime = DateOnly.FromDateTime(request.EndTime).ToDateTime(lastEndTime),
                        ExpectedEndTime = request.ExpectedEndTime,
                        ExpectedStartTime = request.ExpectedStartTime,
                        Notes = "تم أنشاء هذه الزيارة تلقائيًا نظرًا لدخولها في توقيت مختلف",
                        SubscriptionId = request.SubscriptionId,
                        VisitTypeId = request.VisitTypeId
                    };
                    var partVisit = _mapper.Map<Visit>(newVisit) ?? throw new BadHttpRequestException("! عذرًا طلبك غير صالح يرجى إعادة المحاولة");
                    partVisit.TimeShift = anotherTimeShift;
                    partVisit.TimeShiftId = anotherTimeShift.Id;
                    partVisit.TotalMin = timeInAnotherShift;
                    partVisit.Price = CalculatePrice(partVisit);

                    request.EndTime = DateOnly.FromDateTime(request.StartTime).ToDateTime(firstEndTime);
                    var data = _mapper.Map<Visit>(request) ?? throw new BadHttpRequestException("! عذرًا طلبك غير صالح يرجى إعادة المحاولة");
                    data.TimeShift = shift;
                    data.TimeShiftId = shift.Id;
                    var companion = _mapper.Map<IList<Companion>>(request.Companions);
                    data.Companions = companion;
                    var representatives = request.Representatives
                        .Select(p => new RepresentativeVisit() { RepresentativeId = p }).ToList();
                    foreach (var representative in representatives)
                    {
                        if (representative.Representative.CustomerId != subscription.CustomerId)
                            throw new BadHttpRequestException(
                                $"عذرًا المخول {representative.Representative.FullName} ليس مخولًا للزبون {subscription.Customer.Name}");
                    }

                    data.RepresentativesVisits = representatives;
                    data.TotalMin = timeInThisShift;
                    data.Price = CalculatePrice(data);
                    //TODO: Declare Subscription to Decrease Monthly Visit if there a Visit in "While Work" Time Shift.
                    await _dbContext.Visits.AddAsync(data);
                    await _dbContext.Visits.AddAsync(partVisit);
                    await _dbContext.SaveChangesAsync();

                }
                else
                {
                    var data = _mapper.Map<Visit>(request) ?? throw new BadHttpRequestException("! عذرًا طلبك غير صالح يرجى إعادة المحاولة");
                    data.TimeShiftId = shift.Id;
                    data.TimeShift = shift;
                    var companion = _mapper.Map<IList<Companion>>(request.Companions);
                    data.Companions = companion;
                    var representatives = request.Representatives
                        .Select(p => new RepresentativeVisit() { RepresentativeId = p }).ToList();
                    foreach (var representative in representatives)
                    {
                        if (!subscription.Customer.Representatives.Select(p => p.Id)
                                .Contains(representative.RepresentativeId))
                            throw new BadHttpRequestException(
                                $"عذرًا المخول {representative.RepresentativeId}  {subscription.Customer.Name} ليس مخولًا للزبون ");
                    }

                    data.RepresentativesVisits = representatives;
                    data.Price = CalculatePrice(data);
                    await _dbContext.Visits.AddAsync(data);
                    await _dbContext.SaveChangesAsync();
                }

            }
        }
        return new MessageResponse() 
        {
            Msg = "تمت إضافة الزيارة بنجاح!"
        };
    
    }

    public async Task<VisitResponseDto> GetById(Guid id)
    {
        
        var data = await _dbContext.Visits.Where(p => p.Id == id && p.Status != GeneralStatus.Deleted)
            .ProjectTo<VisitResponseDto>(_mapper.ConfigurationProvider).SingleOrDefaultAsync() ??
            throw new BadHttpRequestException("عذرًا لا وجود لفاتورة بهذا الرقم");
        return data;
    }
    public async Task<MessageResponse> Delete(Guid id)
    {
        
        var data = await _dbContext.Visits.Where(p => p.Id == id && p.Status != GeneralStatus.Deleted).SingleOrDefaultAsync() ??
            throw new BadHttpRequestException("عذرًا لا وجود لفاتورة بهذا الرقم");
        data.Status = GeneralStatus.Deleted;
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = $"بنجاح  {id} تم حذف الزيارة "
        };
    }

    public async Task<FetchVisitResponseDto> GetAll(FetchVisitRequestDto request)
    {
        var query = _dbContext.Visits
                .Include(p => p.RepresentativesVisits)
                .ThenInclude(p => p.Representative)
                .Include(p => p.TimeShift)
                .Where(p => p.InvoiceId==null);
        var queryResult = await query .OrderBy(p => p.Id)
                              .Skip(request.PageSize * (request.PageNumber - 1))
                              .Take(request.PageSize)
                              .ProjectTo<VisitResponseDto>(_mapper.ConfigurationProvider)
                              .ToListAsync();
        
        var totalCount = query.Count();
        var totalpages = Math.Ceiling(totalCount / (double)request.PageSize);
        return new FetchVisitResponseDto()
        {
            Content =queryResult,
            CurrentPage = request.PageNumber,
            TotalPages = (int)totalpages
        };

    }

    public async Task<MessageResponse> Lock(Guid id)
    {
        var data = await _dbContext.Visits
           .Where(p => p.Id == id && p.Status == GeneralStatus.Active)
           .FirstOrDefaultAsync() ?? throw new NotFoundException("! عذرًا..لا وجود لزيارة بهذا الرقم او هذه الزيارة مقيدة مسبقًا");
        data.Status = GeneralStatus.LockedByUser;
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "! بنجاح لقد تم تقييد الزيارة"
        };
    }

    public async Task<MessageResponse> Paid(Guid id, Guid invoiceId)
    {
        var data = await _dbContext.Visits
          .Where(p => p.Id == id && p.InvoiceId ==null )
          .FirstOrDefaultAsync() ?? throw new NotFoundException("! عذرًا..لا وجود لزيارة بهذا الرقم او أن هذه الزيارة مضافة الي فاتورة مسبقًا ");
        if (IsLocked(data.Status))
            throw new BadHttpRequestException("! عذرًا..قد تكون هذه الزيارة مقيدة ");
        data.InvoiceId = invoiceId;
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = " لقد تم إضافة هذه الزيارة الي الفاتورة " + invoiceId+" !بنجاح ",
        };
    }

    public async Task<MessageResponse> Unlock(Guid id)
    {
        var data = await _dbContext.Visits
           .Where(p => p.Id == id && p.Status == GeneralStatus.LockedByUser)
           .FirstOrDefaultAsync() ?? throw new NotFoundException("! عذرًا..لا وجود لزيارة بهذا الرقم أو أن هذه الزيارة غير مقيدة ");
        data.Status = GeneralStatus.Active;
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "! لقد تم الغاء تقييد الزيارة بنجاح "
        };
    }

    public async Task<MessageResponse> Update(Guid id, UpdateVisitRequestDto request)
    {
        var data = await _dbContext.Visits
                       .Include(p=> p.RepresentativesVisits)
                       .Include(p => p.Companions)
           .Where(p => p.Id == id && p.InvoiceId == null)
           .FirstOrDefaultAsync() ?? throw new BadRequestException(" يرجى التأكد من صحة رقم الزيارة");
        if (IsLocked(data.Status))
            throw new NotFoundException("! عذرًا..لا وجود لزيارة بهذا الرقم أو أن هذه الزيارة مقيدة ");
        
        var rep = request.Representatives.Select(p => new RepresentativeVisit { RepresentativeId=p }).ToList();
        foreach (var i in rep)
            i.VisitId = id;
        data.RepresentativesVisits= rep;
        if(request.Companions != null)
            data.Companions.Clear();
        data.Companions= _mapper.Map<IList<Companion>>(request.Companions);
        _mapper.Map(request, data);
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "! تم تعديل الزيارة بنجاح"
        };

    }
    /*public async Task<OperationResponse> UpdateCompanion(int visitid, int companionId, UpdateCompanionRequestDto request)
    {
        var data = _dbContext.Companions.Where(p => p.Id == companionId && p.VisitId == visitid).SingleOrDefault();
        if (data == null) throw new BadRequestException(" يرجى التأكد من صحة البيانات المدخلة !");
        var visit = _dbContext.Visits.Where(p => p.Id == visitid ).Select(p => p.Status).Single();
        if(IsLocked(visit))
             throw new NotFoundException("! عذرًا..لا وجود لزيارة بهذا الرقم أو أن هذه الزيارة مقيدة ");

        
        _mapper.Map(request, data);
        await _dbContext.SaveChangesAsync();
        return new OperationResponse()
        {
            Msg = "تم تعديل بيانات المرافق بنجاح !",
            StatusCode = HttpStatusCode.OK
        };

    }*/
        private static bool IsLocked(GeneralStatus status)
        {
        return status switch
        {
            GeneralStatus.Active => false,
            GeneralStatus.LockedByUser => true,
            _ => true,
        };
    }
    private static decimal CalculatePrice(Visit x)
    {
        decimal result;
        if (x.TotalMin == null) return 0;

        if (x.TotalMin.Value.TotalMinutes <= 60.00)
        {
            result = x.TimeShift.PriceForFirstHour;
        }
        else
        {
            var timeAfterFirstHour = x.TotalMin.Value.TotalMinutes - 60;
            var priceByMin=(double) x.TimeShift.PriceForRemainingHour/60;
            result =(decimal)(timeAfterFirstHour * priceByMin) + x.TimeShift.PriceForFirstHour;
        }
        return result;
    }
}

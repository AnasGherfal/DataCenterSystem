using AutoMapper;
using Infrastructure;
using Infrastructure.Models;
using ManagementAPI.Dtos.Customer;
using ManagementAPI.Dtos.Service;
using ManagementAPI.Dtos.Subscriptions;
using ManagementAPI.Dtos.File;

using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using System.Net;

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
        return new OperationResponse() { StatusCode=HttpStatusCode.BadRequest,
            Msg="enter subscription data "
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

    public async Task<FetchSubscriptionResponseDto> GetAllSubscription(int pagenum , int pagesize)
    {
        var Subs_Query = await _dbContext.Subscriptions
            .Include(a=>a.Service)
            .Include(a=>a.Customer)
            .OrderBy(p => p.Id)
            .Skip(pagesize * (pagenum - 1))
            .Take(pagesize)
            .ToListAsync();

        var result = _mapper.Map<List<SubscriptionResponseDto>>(Subs_Query);
        var totalCount = (from Cst in Subs_Query select Cst).Count();
        var totalpages = (int)Math.Ceiling(totalCount / 25.00);

        return new FetchSubscriptionResponseDto()
        {
            Content =result,
            CurrentPage = pagenum,
            TotalPages = pagesize 
        };

    }
    public async Task<OperationResponse> Renew(int id)
    {
        var data = await _dbContext.Subscriptions.SingleOrDefaultAsync(a => a.Id == id);
        if(data == null) return new OperationResponse()
        {
            StatusCode = HttpStatusCode.BadRequest,
            Msg = "no subscription with this number "
        };
        var datetimenow = DateTime.UtcNow;
        var isExpired = datetimenow.CompareTo(data.EndDate);
        if(datetimenow > data.EndDate)
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
                Msg = data.EndDate.ToString()+"   تم تجديدالخدمة بداية من تاريخ الانتهاء للخدمة: " + " بنجاح! ",
                StatusCode = HttpStatusCode.OK
            };
        }
        return new OperationResponse()
        {
            Msg = data.EndDate.ToString()+"   عذرا لم يتم تجديد الخدمة يمكنك التجديد عند انتهاء الخدمة او قبلها ب 30 يوما ",
            StatusCode = HttpStatusCode.BadRequest
        };
    }
    public async Task<OperationResponse> UploadFile(int id,FileDto file)
    {
        var data = await _dbContext.SubscriptionFiles.SingleOrDefaultAsync(a => a.Id == id);
        string fileName;
        var extention = "." + file.File.FileName.Split('.')[file.File.FileName.Split('.').Length - 1];
        fileName = DateTime.UtcNow.Ticks + file.File.FileName;
        var pathBuild = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files");
        if (!Directory.Exists(pathBuild))
        {
            Directory.CreateDirectory(pathBuild);
        }
        var path = Path.Combine(Directory.GetCurrentDirectory(),"Upload\\Files\\"+fileName);
        using (var stream = new FileStream(path, FileMode.Create))
        {
            await file.File.CopyToAsync(stream);
        }
        return new OperationResponse()
        {
            StatusCode = HttpStatusCode.OK,
            Msg = "file uploaded"
        };
    }
}

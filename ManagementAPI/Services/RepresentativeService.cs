using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Constants;
using Infrastructure;
using Infrastructure.Models;

using ManagementAPI.Dtos.Representative;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using Shared.Constants;
using Shared.Dtos;
using System.Linq;
using System.Net;
using Infrastructure.Constants;
using System.Diagnostics.Metrics;
using Shared.Exceptions;

namespace ManagementAPI.Services;

public class RepresentativeService : IRepresentativeService
{
    private readonly DataCenterContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IUploadFileService _upload;
    public RepresentativeService(DataCenterContext dbcontext, IMapper mapper, IUploadFileService upload)
    {
        _dbContext = dbcontext;
        _mapper = mapper;
        _upload = upload;
    }
    public async Task<MessageResponse> Create(CreateRepresentativeRequestDto request)
    {
        var data = _mapper.Map<Representative>(request);
        var customer =await _dbContext.Customers.Include(p => p.Representatives.Where(p=>p.Status!=GeneralStatus.Deleted))
                            .Where(p => p.Id == request.CustomerId).SingleOrDefaultAsync()
                              ?? throw new BadRequestException("عذرًا رقم العميل الذي أدخلته غير صحيح يرجى إعادة المحاولة");
        var representativeCount = customer.Representatives.Count;
        if (representativeCount == 2)
            throw new BadRequestException("عذرًا هذا العميل لديه الحد الأقصى من عدد المخوليين");
        _dbContext.Representatives.Add(data);
        await _upload.Upload(request.FirstFile, EntityType.RepresentativeFile, data);
        await _upload.Upload(request.SecondFile, EntityType.RepresentativeFile, data);
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "تمت اضافة المخول بنجاح"
        };
    }

    public async Task<RepresentativeResponseDto> GetById(Guid id)
    {
        var data =await _dbContext.Representatives.Include(p=>p.Files.Where(x=>x.IsActive!=GeneralStatus.Deleted)).Where(p => p.Id == id && p.Status != GeneralStatus.Deleted)   
            .ProjectTo<RepresentativeResponseDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync()?? throw new NotFoundException("عذرًا لا وجود لعميل أو مخول بهذا الرقم يرجى التأكد وإعادة المحاولة");
        return data;
    }
    public async Task<FileStream> Download(Guid id)
    {
        var data = await _dbContext.RepresentativeFiles.SingleOrDefaultAsync(a => a.Id == id && a.IsActive == GeneralStatus.Active) ?? throw new BadRequestException("عذرًا لا وجود لملفات لهذا العميل..");
        var path = data.FilePath;
        // Check if the file exists.
        if (!File.Exists(path))
        {
            throw new FileNotFoundException("لم يتم العثور على الملف.. ");
        }

        // Open the file for reading.
        return File.OpenRead(path);
    }
    public async Task<FetchRepresentativeResponseDto> GetAll(FetchRepresentativeRequestDto request)
    {
        var query = _dbContext.Representatives.Include(p => p.Files.Where(x=>x.IsActive!=GeneralStatus.Deleted))
                       .ProjectTo<RepresentativeResponseDto>(_mapper.ConfigurationProvider)
                       .Where(p => p.Status != GeneralStatus.Deleted);
        var resultQuery = await query.Skip(request.PageSize * (request.PageNumber - 1))
                                   .Take(request.PageSize)
                                   .ToListAsync();
        var totalCount = query.Count();
        var totalpages = Math.Ceiling(totalCount / (double)request.PageSize);
        return new FetchRepresentativeResponseDto()
        {
            Content = resultQuery,
            CurrentPage = request.PageNumber,
            TotalPages = (int)totalpages
        };
    }
    public async Task<MessageResponse> Delete(Guid id)
    {   
        var data = _dbContext.Representatives.Include(p=> p.Files.Where(x=>x.IsActive==GeneralStatus.Active))
                                      .Where(p => p.Id == id && p.Status == GeneralStatus.Active && p.Customer.Status==GeneralStatus.Active)
                                      .FirstOrDefault()?? throw new NotFoundException("! عذرًا..لا وجود لمخول بهذا الرقم");     
        data.Status = GeneralStatus.Deleted;
        foreach(var file in data.Files)
        {
            file.IsActive = GeneralStatus.Deleted;
        }
        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "!بنجاح " + data.FullName + " : لقد تم حذف المخول",
        };
    }
    public async Task<MessageResponse> Lock(Guid id)
    {
        var data = await _dbContext.Representatives.Include(p=> p.Files.Where(x=>x.IsActive!=GeneralStatus.Deleted))
                                           .Where(p => p.Id == id && p.Status != GeneralStatus.Deleted)
                                           .FirstOrDefaultAsync()?? throw new NotFoundException("! عذرًا..لا وجود لمخول بهذا الرقم");
        if (!IsLocked(data.Status))
        {
            data.Status = GeneralStatus.Locked;
            foreach(var file in data.Files)
            {
                file.IsActive= GeneralStatus.Locked;
            }
            await _dbContext.SaveChangesAsync();
            return new MessageResponse()
            {
                Msg = "!بنجاح " + data.FullName + " : لقد تم تقييد المخول",
            };
        }
        else
            throw new BadRequestException(" ! عذرًا .. هذا المخول مقيد مسبقًا");
    }
    public async Task<MessageResponse> Unlock(Guid id)
    {
        var data= await _dbContext.Representatives.Include(p => p.Files.Where(x => x.IsActive != GeneralStatus.Deleted))
                                                  .Where(p => p.Id == id && p.Status != GeneralStatus.Deleted)
                                                  .FirstOrDefaultAsync()?? throw new NotFoundException("! عذرًا..لا وجود لمخول بهذا الرقم");
        if (IsLocked(data.Status))
        {
            data.Status = GeneralStatus.Active;
            foreach (var file in data.Files)
            {
                file.IsActive = GeneralStatus.Active;
            }
            await _dbContext.SaveChangesAsync();
            return new MessageResponse()
            {
                Msg ="بنجاح " +data.FullName+ " : لقد تم إلغاء التقييد عن المخول",
            };
        }
        else
            throw new BadRequestException( " ! عذرًا .. هذا المخول غير مقيد " );
    }

    public async Task<MessageResponse> Update(Guid id, UpdateRepresentativeRequestDto request)
    {
        var data = await _dbContext.Representatives
                                           .Where(p => p.Id == id && p.Status != GeneralStatus.Deleted)
                                           .FirstOrDefaultAsync() ?? throw new NotFoundException("! عذرًا..لا وجود لمخول بهذا الرقم");
        if (IsLocked(data.Status))
            throw new BadRequestException("! عذرًا..هذا المخول مقيد لا يمكنك تعديل بياناته ");
        var newRepresentative = _mapper.Map<Representative>(request);
        var oldFiles = data.Files.ToList();
        if (oldFiles.Count == 0)
        {
            await _upload.Upload(request.FirstFile, EntityType.RepresentativeFile, newRepresentative);
            await _upload.Upload(request.SecondFile, EntityType.RepresentativeFile, newRepresentative);
        }
        else
        {
            if (request.FirstFile != null)
                foreach (var file in oldFiles)
                {
                    if (file.DocType == request.FirstFile.DocType)
                    {
                        file.IsActive = GeneralStatus.Deleted;
                        await _upload.Upload(request.FirstFile, EntityType.RepresentativeFile, newRepresentative);
                    }

                }
            if (request.SecondFile != null)
                foreach (var file in oldFiles)
                {
                    if (file.DocType == request.SecondFile.DocType)
                    {
                        file.IsActive = GeneralStatus.Deleted;
                        await _upload.Upload(request.SecondFile, EntityType.RepresentativeFile, newRepresentative);
                    }

                }
        }
        _mapper.Map(request, data);

        await _dbContext.SaveChangesAsync();
        return new MessageResponse()
        {
            Msg = "! تم تعديل المخول بنجاح",
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

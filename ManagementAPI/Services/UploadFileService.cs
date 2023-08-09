using AutoMapper;
using Azure.Core;
using Infrastructure;
using Infrastructure.Constants;
using Infrastructure.Models;
using ManagementAPI.Dtos;
using Microsoft.EntityFrameworkCore;
using Shared.Constants;
using Shared.Dtos;
using Shared.Exceptions;

namespace ManagementAPI.Services;

public class UploadFileService : IUploadFileService
{
    private readonly IConfiguration _config;
    private readonly IMapper _mapper;
    private readonly DataCenterContext _dbContext;
    public UploadFileService(IConfiguration config,IMapper mapper,DataCenterContext dbContext)
    {
        _config = config;
        _mapper = mapper;
        _dbContext = dbContext;
    }
    public async Task<MessageResponse> Upload(FileRequestDto request, EntityType type, Object objct)
    {
        //TODO:["Important!"] File Model Should Have A FilePath with FileNAme Prop
       
            var path = GetFilePath(type, objct);
            var ext = Path.GetExtension(request.File.FileName);
            var fullFileName = ToTrustedFileName(type,objct,(DocType)request.DocType,ext);

            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            string fullPath = Path.Combine(path, fullFileName);
            if (System.IO.File.Exists(fullPath))
            {

            }
            using (FileStream stream = System.IO.File.Create(fullPath))
            {
                switch (type)
                {
                    case EntityType.CustomerFile:
                        {
                            await request.File.CopyToAsync(stream);
                            var dataFile = _mapper.Map<CustomerFile>(request.File);
                            dataFile.Filename = fullFileName;
                            dataFile.FilePath = fullPath;
                        dataFile.IsActive = GeneralStatus.Active;
                            dataFile.DocType = request.DocType;
                            dataFile.Customer = (Customer)objct;
                            _dbContext.CustomerFiles.Add(dataFile);
                            await _dbContext.SaveChangesAsync();
                            break;
                        }
                    case EntityType.SubscriptionFile:
                        {
                            await request.File.CopyToAsync(stream);
                            var dataFile = _mapper.Map<SubscriptionFile>(request.File);
                            dataFile.FileName = fullFileName;
                            dataFile.FilePath = fullPath;
                        dataFile.IsActive = GeneralStatus.Active;
                        dataFile.Subscription = (Subscription)objct;
                            _dbContext.SubscriptionFiles.Add(dataFile);
                            await _dbContext.SaveChangesAsync();
                            break;
                        }
                    case EntityType.RepresentativeFile:
                        {
                            await request.File.CopyToAsync(stream);
                            var dataFile = _mapper.Map<RepresentativeFile>(request.File);
                            dataFile.Filename = fullFileName;
                            dataFile.FilePath = fullPath;
                        dataFile.IsActive = GeneralStatus.Active;
                        dataFile.DocType = request.DocType;
                            dataFile.Representative = (Representative)objct;
                            _dbContext.RepresentativeFiles.Add(dataFile);
                            await _dbContext.SaveChangesAsync();
                            break;
                        }
                    default:
                        throw new BadRequestException("عذرًا هناك خطأ في إضافة الملف, يرجى الإتصال بالدعم الفني");
                }


            }
       
        return new MessageResponse() { Msg = "تمت إضافة الملف بنجاح" };
           
        }
    
    private string GetFilePath(EntityType type,object obj)
    {
        string name="";
        switch(type)
        {
                case EntityType.CustomerFile:
                var customer = (Customer)obj;
                name= customer.Name;
                break;
                case EntityType.SubscriptionFile:
                var subscription = (Subscription)obj;
                name = subscription.Id + "Subcriptions Files";
                break;
                case EntityType.RepresentativeFile:
                var representative = (Representative)obj;
                name = representative.FullName + " Files";
                break;
            default:
                name = "Unknown Files";
                break;
        }
        return _config.GetValue<string>("Storage:Customer") + "\\" + DateOnly.FromDateTime(DateTime.UtcNow).Year.ToString() + "\\" + $"{name}\\";

    }
    private static string ToTrustedFileName(EntityType type, object obj, DocType docType, string ext)
    {
        string name = "";
        switch (type)
        {
            case EntityType.CustomerFile:
                var customer = (Customer)obj;
                name = customer.Name + $" {docType}";
                break;
            case EntityType.SubscriptionFile:
                var subscription = (Subscription)obj;
                name = subscription.Id.ToString();
                break;
            case EntityType.RepresentativeFile:
                var representative = (Representative)obj;
                name = representative.FullName + $" {docType}" ;
                break;
            default:
                name = "Unknown Files";
                break;
        }
        return name + ext;
    }
}
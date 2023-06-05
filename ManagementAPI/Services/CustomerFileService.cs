using AutoMapper;
using Infrastructure;
using Infrastructure.Models;
using ManagementAPI.Dtos.Customer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Constants;
using Shared.Dtos;
using System.IO;
using System.Net;

namespace ManagementAPI.Services;

public class CustomerFileService:ICustomerFileService
{
    private readonly DataCenterContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _env;
    public CustomerFileService(DataCenterContext dbContext, IMapper mapper, IWebHostEnvironment env)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _env = env;
    }

   
}
  /*  public async Task<OperationResponse> Upload(CustomerFileRequestDto request) 
    {
        var file = request.File;
        string notTrustedFileName = Path.GetFileNameWithoutExtension(file.FileName);
        string trustedFileName = ToTrustedFileName(data.Name,request.DocType);
        string fileExt = Path.GetExtension(file.FileName);
        string fullFileName = trustedFileName + fileExt;
        string path = GetFilePath();
        if (!System.IO.Directory.Exists(path))
        {
            System.IO.Directory.CreateDirectory(path);
        }
        string fullPath = Path.Combine(path, fullFileName);
        if (System.IO.File.Exists(fullPath))
            return new OperationResponse() 
            {
                Msg="! عذرًا ولكن هذا الملف موجود مسبقًا",
                StatusCode=HttpStatusCode.BadRequest
            };
        using (FileStream stream = System.IO.File.Create(fullPath))
        {
            await file.CopyToAsync(stream);
            var dataFile = _mapper.Map<CustomerFile>(request); 
            dataFile.Filename = trustedFileName;
            _dbContext.CustomerFiles.Add(dataFile);
            await _dbContext.SaveChangesAsync();
            return new OperationResponse() {
               Msg= "تمت إضافة الملف بنجاح",
               StatusCode=HttpStatusCode.OK
            };
        }
    }*/
    /*public async Task<OperationResponse> Delete(int id)
    {
        
        var data = await _dbContext.CustomerFiles.Where(p => p.CustomerId ==id).FirstOrDefaultAsync();
        if (data == null) 
            return new OperationResponse()
            {
                Msg = "عذرًا..لا وجود لعميل بهذا الرقم",
                StatusCode = HttpStatusCode.BadRequest,
            };

        string fullFileName = data.Filename + data.FileType;
        string path = GetFilePath();
        if (!System.IO.Directory.Exists(path))
        {
            System.IO.Directory.CreateDirectory(path);
        }
        string fullPath = Path.Combine(path, fullFileName);
        if (System.IO.File.Exists(fullPath))
            return new OperationResponse()
            {
                Msg = "! عذرًا ولكن هذا الملف موجود مسبقًا",
                StatusCode = HttpStatusCode.BadRequest
            };
        using (FileStream stream = System.IO.File.Create(fullPath))
        {
            await file.CopyToAsync(stream);
            await _dbContext.SaveChangesAsync();
            return new OperationResponse()
            {
                Msg = "FullPath:: " + fullPath + " Ex::::  " + fileExt + "  Path::: " + path + " TrustedFileName::   " + trustedFileName + "  NotTrustedFileName::  " + notTrustedFileName,
                StatusCode = HttpStatusCode.OK
            };
        }
    }
    private string GetFilePath()
    {
        return _env.WebRootPath + "\\Uploads\\";
    }
    private string ToTrustedFileName(string customerName,DocType type)
    {
        return Path.Combine(customerName+"-"+type.ToString());
    }
}
*/

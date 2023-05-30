using AutoMapper;
using Azure.Core;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Shared.Constants;
using Shared.Dtos;
using System.Diagnostics.Metrics;

namespace ManagementAPI.Services;

public class FileUploadService : IFileUploadService
{
    private readonly IWebHostEnvironment _env;
    private readonly IMapper _mapper;
    public FileUploadService(IWebHostEnvironment env, IMapper mapper)
    {
        _env = env;
        _mapper = mapper;
    }
    public  FormFile Upload(FormFile file)
    {
        var path = GetFilePath();
        var ext = Path.GetExtension(file.FileName);
        var fullFileName = ToTrustedFileName(ext);
        
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
           file.CopyToAsync(stream);
            return file;
          
            //L
        }
        

    }
    private string GetFilePath()
    {
        return _env.WebRootPath + "\\Uploads\\";
    }
    private string ToTrustedFileName(string ext)
    {
        return Guid.NewGuid().ToString()+ext;
    }
}


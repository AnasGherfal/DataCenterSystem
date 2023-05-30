using Infrastructure.Models;
using ManagementAPI.Dtos.Customer;
using Shared.Dtos;

namespace ManagementAPI.Services;

public interface IFileUploadService
{
    public FormFile Upload(FormFile file);
}

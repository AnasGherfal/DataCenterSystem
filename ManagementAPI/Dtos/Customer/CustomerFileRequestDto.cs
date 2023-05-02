using Infrastructure.Models;
using Shared.Constants;

namespace ManagementAPI.Dtos.Customer;

public class CustomerFileRequestDto
{
    public IFormFile File { get; set; }
    public DocType DocType { get; set; }
    public int CustomerId { get; set; }
}

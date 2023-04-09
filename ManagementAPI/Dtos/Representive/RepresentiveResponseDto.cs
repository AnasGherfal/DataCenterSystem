using Infrastructure.Models;
using ManagementAPI.Dtos.Customer;
using Shared.Constants;

namespace ManagementAPI.Dtos.Representive;

public class VisitResponseDto
{
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public TimeSpan? TotalMin { get; set; }
    public decimal Price { get; set; }
    public string? Notes { get; set; }
    public string CustomerName { get; set;}   
}

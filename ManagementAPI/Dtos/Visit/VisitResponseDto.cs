﻿using ManagementAPI.Dtos.Companion;
using ManagementAPI.Dtos.Representive;
using Shared.Constants;

namespace ManagementAPI.Dtos.Visit;
public class VisitResponseDto
{
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public TimeSpan? TotalMin { get; set; }
    public decimal Price { get; set; }
    public string? Notes { get; set; }
    public string TimeShift { get; set; }
    public string  CustomerName { get; set; }
    public IList<RepresentiveResponseDto> Representives { get; set; }
    public IList<CompanionResponseDto> Companions { get; set; }
    public string VisitType { get; set; }
    public int? InvoiceId { get; set; }
    
}

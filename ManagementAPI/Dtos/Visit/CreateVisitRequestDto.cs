﻿using ManagementAPI.Dtos.Representive;
using ManagementAPI.Dtos.Companion;

namespace ManagementAPI.Dtos.Visit;
using Infrastructure.Models;


public class CreateVisitRequestDto
{
    public DateTime? ExpectedStartTime { get; set; }
    public DateTime? ExpectedEndTime { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string? Notes { get; set; }
    public short VisitTypeId { get; set; }
   // public int TimeShiftId { get; set; }
    public int? InvoiceId { get; set; }
    public int SubscriptionId { get; set; }
    public IList<CreateCompanionRequestDto> Companions { get; set; }
    public IList<int> Representives { get; set; }
}

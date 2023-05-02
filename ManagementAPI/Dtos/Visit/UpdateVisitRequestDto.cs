﻿using Common.Helpers;
using FluentValidation;

namespace ManagementAPI.Dtos.Visit;
public class UpdateVisitRequestDto
{
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public TimeSpan? TotalMin { get; set; }
    public decimal Price { get; set; }
    public string? Notes { get; set; }
    public short VisitTypeId { get; set; }
    public int VisitShiftId { get; set; }
    public int? InvoiceId { get; set; }
}
public class UpdateVisitValidator : AbstractValidator<UpdateVisitRequestDto>
{
    public UpdateVisitValidator()
    {

     
    }
}

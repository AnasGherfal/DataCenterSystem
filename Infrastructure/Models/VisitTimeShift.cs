﻿using Infrastructure.Constants;

namespace Infrastructure.Models;

public class VisitTimeShift : BaseModel
{
    public Guid Id{ get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal PriceForFirstHour { get; set; }
    public decimal PriceForRemainingHour { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public GeneralStatus Status { get; set; }
}

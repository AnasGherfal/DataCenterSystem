﻿namespace Infrastructure.Models;

public class Visit : BaseModel
{ 
    public int Id { get; set; }
    public DateTime? ExpectedStartTime { get; set; }
    public DateTime? ExpectedEndTime { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public TimeSpan? TotalMin { get; set; }
    public decimal Price { get; set; }
    public string? Notes { get; set; } 
    public short VisitTypeId { get; set; }
    public int SubscriptionId { get; set; }
    public int TimeShiftId { get; set; }
    public int? InvoiceId { get; set; }


    //---------------Relations

    public VisitType VisitType { get; set; } 
    public ICollection<Companion> Companions { get; set; }=new List<Companion>();
    public Invoice? Invoice { get; set; }
    public ICollection<Representive> Representives { get; set; } = new List<Representive>();
    public VisitTimeShift TimeShift { get; set; } 

}
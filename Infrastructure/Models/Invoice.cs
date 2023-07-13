using Infrastructure.Constants;

namespace Infrastructure.Models;

public class Invoice : BaseModel
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public decimal TotalAmount { get; set; }
    public string? Description { get; set; }
    public string? InvoiceNo { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public GeneralStatus Status { get; set; }
    public bool IsPaid { get; set; }
    public Guid SubscriptionId { get; set; }
    //----Realation----------
    public Subscription Subscription { get; set; } = default!;
    public ICollection<Visit> Visits { get; set; }= new List<Visit>();





}

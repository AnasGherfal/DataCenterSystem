using Infrastructure.Constants;

namespace Infrastructure.Models;

public class Invoice : BaseModel
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public decimal TotalAmount { get; set; }
    public string? Description { get; set; }
    public string? InvoiceNo { get; set; }
    //public string? Notes { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public GeneralStatus Status { get; set; }
    public bool IsPaid { get; set; }
    public int SubscriptionId { get; set; }
    //----Realation----------
    public Subscription Subscription { get; set; }
    public ICollection<Visit> Visits { get; set; }= new List<Visit>();





}

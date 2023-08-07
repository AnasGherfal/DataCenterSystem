using Infrastructure.Constants;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    public class Subscription : BaseModel
    {
        public Guid Id { get; set; }
        public Guid ServiceId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public GeneralStatus Status { get; set; }
        public int MonthlyVisits{ get; set; }
        public decimal? TotalPrice { get; set; }
        //----------relations
        public Service Service { get; set; } = default!;
        public Customer Customer { get; set; } = default!;
        public SubscriptionFile SubscriptionFile { get; set; } = default!;
        public ICollection<Visit> Visits { get; set; } = new List<Visit>();
        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();   
        public ICollection<AdditionalPower> AdditionalPowers { get; set; } = new List<AdditionalPower>();

    }
}

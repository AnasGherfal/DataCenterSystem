using Infrastructure.Constants;
using System.ComponentModel.DataAnnotations.Schema;
using Infrastructure.Audits.Subscription;

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
        public decimal? TotalPrice { get; set; }
        //----------relations
        public Service Service { get; set; } = default!;
        public Customer Customer { get; set; } = default!;
        public SubscriptionFile SubscriptionFile { get; set; } = default!;
        public ICollection<Visit> Visits { get; set; } = new List<Visit>();
        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();   
        public ICollection<AdditionalPower> AdditionalPowers { get; set; } = new List<AdditionalPower>();

        
        public static Subscription Create(SubscriptionCreatedAudit @event)
        {
            return new Subscription()
            {
                Id = @event.AggregateId,
                ServiceId = @event.Data.ServiceId,
                CustomerId = @event.Data.CustomerId,
                StartDate = @event.Data.StartDate,
                EndDate = @event.Data.EndDate,
                Status = GeneralStatus.Active,
                CreatedOn = @event.DateTime,
                // CreatedById = @event.UserId,
                SubscriptionFile = new SubscriptionFile()
                {
                    Id = @event.Data.FileIdentifier,
                    FileType = @event.Data.FileType,
                    FilePath = @event.Data.File,
                    IsActive = GeneralStatus.Active,
                    CreatedOn = @event.DateTime,
                    // CreatedById = @event.UserId,
                }
            };
        }
        
        public void Apply(SubscriptionRenewedAudit @event)
        {
            EndDate = @event.DateTime.AddYears(1);
        }
        
        public void Apply(SubscriptionLockedAudit @event)
        {
            Status = GeneralStatus.Locked;
        }
    
        public void Apply(SubscriptionUnlockedAudit @event)
        {
            Status = GeneralStatus.Active;
        }
    
        public void Apply(SubscriptionDeletedAudit @event)
        {
            Status = GeneralStatus.Deleted;
            SubscriptionFile.IsActive = GeneralStatus.Deleted;
        }
        
        public void Apply(SubscriptionFileUpdatedAudit @event)
        {
            SubscriptionFile.FileType = @event.Data.FileType;
            SubscriptionFile.FilePath = @event.Data.File;
            SubscriptionFile.IsActive = GeneralStatus.Active;
            SubscriptionFile.CreatedOn = @event.DateTime;
        }
    }
}

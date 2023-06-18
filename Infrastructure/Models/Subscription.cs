﻿using Infrastructure.Constants;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    public class Subscription : BaseModel
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int CustomerId { get; set; }
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

    }
}

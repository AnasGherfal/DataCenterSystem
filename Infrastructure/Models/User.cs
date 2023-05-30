namespace Infrastructure.Models; 

public class User
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public short Status { get; set; }
    public int EmpId { get; set; }

    public DateTime CreatedOn { get; set; }


    public int? CreatedById { get; set; }

    //------Relation
    //  [NotMapped]
    public User? CreatedBy { get; set; }

    //Realation
    // public ICollection<Permission> Permissions { get; set; } = new List<Permission>();

    public ICollection<User> UsersCreatedBy { get; set; } = default!;
    public ICollection<AdditionalPower> AdditionalPowersCreatedBy { get; set; } = default!;
    public ICollection<Customer> CustomersCreatedBy { get; set; } = default!;
    public ICollection<Companion> CompanionsCreatedBy { get; set; } = default!;
    public ICollection<CustomerFile> CustomerFilesCreatedBy { get; set; } = default!;
    public ICollection<Invoice> InvoicesCreatedBy { get; set; } = default!;
    public ICollection<Representative> RepresentativesCreatedBy { get; set; } = default!;
    public ICollection<RepresentativeFile> RepresentativeFilesCreatedBy { get; set; } = default!;
    public ICollection<Service> ServicesCreatedBy { get; set; } = default!;
    public ICollection<Subscription> SubscriptionsCreatedBy { get; set; } = default!;
    public ICollection<SubscriptionFile> SubscriptionFilesCreatedBy { get; set; } = default!;
    public ICollection<TransactionHistory> TransactionHistorysCreatedBy { get; set; } = default!;
    public ICollection<Visit> VisitsCreatedBy { get; set; } = default!;
    public ICollection<VisitTimeShift> VisitTimeShiftsCreatedBy { get; set; } = default!;

}

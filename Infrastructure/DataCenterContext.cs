using Infrastructure.Audits.Abstracts;
using Infrastructure.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Infrastructure;

public class DataCenterContext : IdentityDbContext<Admin, AdminRole, Guid>
{
    public DataCenterContext(DbContextOptions<DataCenterContext> options) : base(options)
    {
    }
    
    public DbSet<Audit> Audits => Set<Audit>();
    public DbSet<AdditionalPower> AdditionalPowers => Set<AdditionalPower>();
    public DbSet<Companion> Companions => Set<Companion>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<CustomerFile> CustomerFiles => Set<CustomerFile>();
    // public DbSet<Entity> Entities => Set<Entity>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    // public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<Representative> Representatives => Set<Representative>();
    public DbSet<RepresentativeFile> RepresentativeFiles => Set<RepresentativeFile>();
    public DbSet<Service> Services => Set<Service>();
    public DbSet<Subscription> Subscriptions => Set<Subscription>();
    public DbSet<SubscriptionFile> SubscriptionFiles => Set<SubscriptionFile>();
    public DbSet<TransactionHistory> TransactionHistories => Set<TransactionHistory>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Visit> Visits => Set<Visit>();
    public DbSet<RepresentativeVisit> RepresentativeVisits => Set<RepresentativeVisit>();
    public DbSet<VisitTimeShift> VisitTimeShifts => Set<VisitTimeShift>();
    public DbSet<VisitType> VisitTypes => Set<VisitType>();

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<DateTime>().HaveColumnType("datetime");
        configurationBuilder.Properties<TimeSpan>().HaveColumnType("time");
        base.ConfigureConventions(configurationBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        this.AddAuditBuilder(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataCenterContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
using Infrastructure.Builders;
using Infrastructure.Configurations;
using Infrastructure.Events.Abstracts;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Admin = Infrastructure.Entities.Admin;
using AdminRole = Infrastructure.Entities.AdminRole;
using Companion = Infrastructure.Entities.Companion;
using Customer = Infrastructure.Entities.Customer;
using Invoice = Infrastructure.Entities.Invoice;
using Representative = Infrastructure.Entities.Representative;
using Service = Infrastructure.Entities.Service;
using Subscription = Infrastructure.Entities.Subscription;
using User = Infrastructure.Entities.User;
using Visit = Infrastructure.Entities.Visit;

namespace Infrastructure;

public class DataCenterContext : IdentityDbContext<Admin, AdminRole, Guid>
{
    public DataCenterContext(DbContextOptions<DataCenterContext> options) : base(options)
    {
    }
    
    public DbSet<Event> Audits => Set<Event>();
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
        this.AddEventBuilder(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataCenterContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
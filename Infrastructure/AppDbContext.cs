using System.Security.Claims;
using Common.Constants;
using Infrastructure.Builders;
using Infrastructure.Constants;
using Infrastructure.Entities;
using Infrastructure.Entities.Mappers;
using Infrastructure.Events.Abstracts;
using Infrastructure.Events.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Infrastructure;

public class AppDbContext : IdentityDbContext<Admin, AdminRole, Guid>
{
    public DbSet<Event> Events => Set<Event>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Service> Services => Set<Service>();
    public DbSet<Subscription> Subscriptions => Set<Subscription>();
    public DbSet<Representative> Representatives => Set<Representative>();
    public DbSet<TimeShift> TimeShifts => Set<TimeShift>();
    public DbSet<Visit> Visits => Set<Visit>();
    public DbSet<Invoice> Invoices => Set<Invoice>();

    //Mappers
    public DbSet<DocumentForCustomer> DocumentForCustomers => Set<DocumentForCustomer>();
    public DbSet<DocumentForSubscription> DocumentForSubscriptions => Set<DocumentForSubscription>();
    public DbSet<DocumentForRepresentative> DocumentForRepresentatives => Set<DocumentForRepresentative>();
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    
    // public DbSet<AdditionalPower> AdditionalPowers => Set<AdditionalPower>();
    // public DbSet<Companion> Companions => Set<Companion>();
    // public DbSet<CustomerFile> CustomerFiles => Set<CustomerFile>();
    // // public DbSet<Entity> Entities => Set<Entity>();
    // public DbSet<Invoice> Invoices => Set<Invoice>();
    // // public DbSet<Permission> Permissions => Set<Permission>();
    // public DbSet<Representative> Representatives => Set<Representative>();
    // public DbSet<RepresentativeFile> RepresentativeFiles => Set<RepresentativeFile>();
    //
    // public DbSet<Subscription> Subscriptions => Set<Subscription>();
    // public DbSet<SubscriptionFile> SubscriptionFiles => Set<SubscriptionFile>();
    // public DbSet<TransactionHistory> TransactionHistories => Set<TransactionHistory>();
    // public DbSet<User> Users => Set<User>();
    // public DbSet<Visit> Visits => Set<Visit>();
    // public DbSet<RepresentativeVisit> RepresentativeVisits => Set<RepresentativeVisit>();
    // public DbSet<VisitTimeShift> VisitTimeShifts => Set<VisitTimeShift>();
    // public DbSet<VisitType> VisitTypes => Set<VisitType>();

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<DateTime>().HaveColumnType("datetime");
        configurationBuilder.Properties<TimeSpan>().HaveColumnType("time");
        base.ConfigureConventions(configurationBuilder);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        this.AddEventBuilder(modelBuilder);
        this.AddCustomerBuilder(modelBuilder);
        this.AddRepresentativeBuilder(modelBuilder);
        this.AddServiceBuilder(modelBuilder);
        this.AddSubscriptionBuilder(modelBuilder);
        this.AddInvoiceBuilder(modelBuilder);
        this.AddTimeShiftBuilder(modelBuilder);
        this.AddVisitBuilder(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }
    
    public void InitAdmin()
    {
        
        var @event = new AdminCreatedEvent(Guid.Empty, Guid.NewGuid(), new AdminCreatedEventData()
        {
            FullName = "Super Admin",
            UserName = Guid.NewGuid().ToString().Replace("-", ""),
            SecurityStamp = Guid.NewGuid().ToString(),
            Email = "admin@ltt.ly",
            Permissions = SystemPermissions.SuperAdmin,
            EmpId = 1,
            Password = "Password12345"
        });
        var userManager = new UserManager<Admin>(
            new UserStore<Admin, IdentityRole<Guid>, AppDbContext, Guid>(this),
            null,
            new PasswordHasher<Admin>(),
            null,
            null,
            null,
            null,
            null,
            null
        );
        var user = userManager.FindByEmailAsync(@event.Data.Email).Result;
        if (user != null) return;
        user = new Admin();
        user.Apply(@event);
        userManager.CreateAsync(user, @event.Data.Password).Wait();
        var authClaims = new List<Claim>
        {
            new(ClaimsKey.IdentityId.Key(), user.Id.ToString()),
            new(ClaimsKey.DisplayName.Key(), user.DisplayName),
            new(ClaimsKey.Email.Key(), user.Email ?? ""),
            new(ClaimsKey.Permissions.Key(), user.Permissions.ToString("D")),
            new(ClaimsKey.EmailVerified.Key(), user.EmailConfirmed.ToString()),
        };
        userManager.AddClaimsAsync(user, authClaims).Wait();
    }
}
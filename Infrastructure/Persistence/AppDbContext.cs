using System.Security.Claims;
using Core.Constants;
using Core.Entities;
using Core.Entities.Mappers;
using Core.Events.Abstracts;
using Core.Events.Admin;
using Infrastructure.Persistence.Builders;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class AppDbContext : IdentityDbContext<Account, AccountRole, Guid>
{
    public DbSet<Event> Events => Set<Event>();
    public DbSet<Service> Services => Set<Service>();
    public DbSet<Admin> Admins => Set<Admin>();
    public DbSet<Customer> Customers => Set<Customer>();
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
    
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<DateTime>().HaveColumnType("datetime");
        configurationBuilder.Properties<TimeSpan>().HaveColumnType("time");
        base.ConfigureConventions(configurationBuilder);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        this.AddEventBuilder(modelBuilder);
        this.AddAdminBuilder(modelBuilder);
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
        
        var @event = new AdminCreatedEvent(Guid.Empty, Guid.Empty, new AdminCreatedEventData()
        {
            FullName = "Super Admin",
            UserName = Guid.NewGuid().ToString().Replace("-", ""),
            SecurityStamp = Guid.NewGuid().ToString(),
            Email = "admin@ltt.ly",
            Permissions = SystemPermissions.SuperAdmin,
            EmpId = 1,
            Password = "Password12345"
        });
        
        var userManager = new UserManager<Account>(
            new UserStore<Account, IdentityRole<Guid>, AppDbContext, Guid>(this),
            null,
            new PasswordHasher<Account>(),
            null,
            null,
            null,
            null,
            null,
            null
        );
        var user = userManager.FindByEmailAsync(@event.Data.Email).Result;
        if (user != null) return;
        user = new Account();
        user.Apply(@event);
        userManager.CreateAsync(user, @event.Data.Password).Wait();
        var authClaims = new List<Claim>
        {
            new(ClaimsKey.IdentityId.Key(), user.Id.ToString()),
            new(ClaimsKey.DisplayName.Key(), user.DisplayName),
            new(ClaimsKey.Email.Key(), user.Email ?? ""),
            new(ClaimsKey.Permissions.Key(), user.Permissions.ToString("D")),
            new(ClaimsKey.EmailVerified.Key(), user.EmailConfirmed.ToString()),
            new(ClaimsKey.UserType.Key(), AccountType.Admin.ToString("D")),
        };
        userManager.AddClaimsAsync(user, authClaims).Wait();
    }
}
using Infrastructure.EntityConfigurations;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ServiceDbContext:DbContext
{
    public ServiceDbContext(DbContextOptions<ServiceDbContext> options) : base(options)
    {

    }
    public DbSet<AdditionalPower> AdditionalPowers { get; set; }
 //   public DbSet<BaseModel> BaseModels { get; set; }
    public DbSet<Companion>  Companions { get; set; }
    public DbSet<Customer> Customers  { get; set; }
    public DbSet<CustomerFile> CustomerFiles { get; set; }
    public DbSet<Entity> Entities  { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<Permission> Permissions  { get; set; }
    public DbSet<Representive>  Representives { get; set; }
    public DbSet<RepresentiveFile> RepresentiveFiles  { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Subscription> Subscriptions  { get; set; }
    public DbSet<SubscriptionFile> SubscriptionFiles  { get; set; }
    public DbSet<TransactionHistory> TransactionHistories  { get; set; }
    public DbSet<User> Users  { get; set; }
    public DbSet<Visit> Visits  { get; set; }
    public DbSet<VisitTimeShift> VisitTimeShifts  { get; set; }
    public DbSet<VisitType> VisitTypes  { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AdditionalPowerConfig());
     //   modelBuilder.ApplyConfiguration(new BaseModelConfig());
        modelBuilder.ApplyConfiguration(new CompanionConfig()  );
        modelBuilder.ApplyConfiguration(new CustomerConfig()   );
        modelBuilder.ApplyConfiguration(new CustomerFileConfig()    );
        modelBuilder.ApplyConfiguration(new  EntityConfig()  );
        modelBuilder.ApplyConfiguration(new InvoiceConfig()   );
        modelBuilder.ApplyConfiguration(new PermissionConfig()   );
        modelBuilder.ApplyConfiguration(new RepresentiveConfig()   );
        modelBuilder.ApplyConfiguration(new RepresentiveFileConfig()   );
        modelBuilder.ApplyConfiguration(new ServiceConfig()   );
        modelBuilder.ApplyConfiguration(new SubscriptionConfig()   );
        modelBuilder.ApplyConfiguration(new SubscriptionFileConfig()   );
        modelBuilder.ApplyConfiguration(new TransactionHistoryConfig  ()  );
        modelBuilder.ApplyConfiguration(new UserConfig()    );
        modelBuilder.ApplyConfiguration(new VisitConfig()   );
        modelBuilder.ApplyConfiguration(new VisitTimeShiftConfig()   );
        modelBuilder.ApplyConfiguration(new VisitTypeConfig() );















    }

















}
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Infrastructure;

public class ServiceDbContext:DbContext
{
    public ServiceDbContext(DbContextOptions<ServiceDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    
        modelBuilder.Entity<User>().HasOne(u => u.CreatedBy)
              .WithMany(u => u.UsersCreatedBy)
              .HasForeignKey(u => u.CreatedById)
              .OnDelete(DeleteBehavior.NoAction);
              
        base.OnModelCreating(modelBuilder);

    }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<AdditionalPower> AdditionalPowers { get; set; }

}
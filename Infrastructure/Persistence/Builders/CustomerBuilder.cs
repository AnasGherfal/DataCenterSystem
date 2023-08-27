using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Builders;

public static class CustomerBuilder
{
    public static void AddCustomerBuilder(this DbContext dbContext,  ModelBuilder builder)
    {
        builder.Entity<Customer>(b => {
            b.HasQueryFilter(p => !p.IsDeleted);
            b.HasKey(p => p.Id);
            b.Property(p => p.CreatedOn).IsRequired().HasDefaultValue(DateTime.UtcNow);
            b.Property(p => p.UpdatedOn).IsRequired().HasDefaultValue(DateTime.UtcNow);
            b.Property(p => p.Sequence).IsRequired().HasDefaultValue(1);
            b.Property(a => a.RowVersion).IsRequired().IsRowVersion();
            b.Property(a => a.IsDeleted).IsRequired().HasDefaultValue(false);
            b.ToTable("Customers");
        });
    }
}
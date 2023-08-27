using Infrastructure.Models;
namespace Infrastructure.Configurations;

public class CustomerConfig : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.Property(p => p.Name).HasMaxLength(200).IsRequired();
        builder.Property(p => p.PrimaryPhone).HasMaxLength(20).IsRequired();
        builder.Property(p => p.SecondaryPhone).HasMaxLength(20);
        builder.Property(p => p.Email).HasMaxLength(320).IsRequired();
        builder.Property(p => p.Address).HasMaxLength(320);
        builder.Property(p => p.Status).IsRequired();

        builder.HasOne(a => a.CreatedBy)
               .WithMany(u => u.CustomersCreatedBy)
               .HasForeignKey(a => a.CreatedById)
               .OnDelete(DeleteBehavior.ClientSetNull);

    }
}

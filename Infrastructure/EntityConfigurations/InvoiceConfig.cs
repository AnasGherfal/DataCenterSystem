using Infrastructure.Models;

namespace Infrastructure.EntityConfigurations;


public class InvoiceConfig : IEntityTypeConfiguration<Invoice>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Invoice> builder)
    {
        builder.Property(p => p.Date).IsRequired();
        builder.Property(p => p.TotalAmount).HasPrecision(14,2).IsRequired();
        builder.Property(p => p.Description).HasMaxLength(450);
        builder.Property(p => p.InvoiceNo).HasMaxLength(40);
        builder.Property(p => p.StartDate).IsRequired();
        builder.Property(p => p.EndDate).IsRequired();
        builder.Property(p => p.Status).IsRequired();
         builder.Property(p => p.SubscriptionId).IsRequired();

    }
}

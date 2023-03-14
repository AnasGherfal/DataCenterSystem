using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Models;

namespace Infrastructure.EntityConfigurations;;

public class SubscriptionFileConfig : IEntityTypeConfiguration<SubscriptionFile>
{
    public void Configure(EntityTypeBuilder<SubscriptionFile> builder)
    {
       
    }
}


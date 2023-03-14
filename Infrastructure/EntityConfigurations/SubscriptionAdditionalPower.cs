using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Models;

namespace Infrastructure.EntityConfigurations;

public class SubscriptionAdditionalPowerConfig : IEntityTypeConfiguration<SubscriptionAdditionalPower>
{
    public void Configure(EntityTypeBuilder<SubscriptionAdditionalPower> builder)
    {
        

    }
}


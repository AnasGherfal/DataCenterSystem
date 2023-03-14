using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Models;

namespace Infrastructure.EntityConfigurations;

public class CustomerFileConfig : IEntityTypeConfiguration<CustomerFile>
{
    public void Configure(EntityTypeBuilder<CustomerFile> builder)
    {
        builder.HasKey(a => a.Id);
        throw new NotImplementedException();
       
    }
}
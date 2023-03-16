using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class BaseModelConfig : IEntityTypeConfiguration<BaseModel>
{
    public void Configure(EntityTypeBuilder<BaseModel> builder)
    {

    }
}


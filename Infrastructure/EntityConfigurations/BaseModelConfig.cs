using Infrastructure.Models;

namespace Infrastructure.EntityConfigurations;

public class BaseModelConfig : IEntityTypeConfiguration<BaseModel>
{
    public void Configure(EntityTypeBuilder<BaseModel> builder)
    {

    }
}


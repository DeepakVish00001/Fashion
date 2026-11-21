using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config;

public class ProductCategoryConfigration : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(100);

        builder.HasIndex(x => x.Name).IsUnique();

        builder.Property(x => x.Description).HasMaxLength(500);
        
    }
}

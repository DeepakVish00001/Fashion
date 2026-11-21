using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config;

public class ProductBrandConfigration : IEntityTypeConfiguration<ProductBrand>
{
    public void Configure(EntityTypeBuilder<ProductBrand> builder)
    {
        builder.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(100);

        builder.HasIndex(x => x.Name).IsUnique();

        builder.Property(x => x.Description).HasMaxLength(500);

        builder.HasOne(c => c.ProductCategory)
            .WithMany(c => c.ProductBrands)
            .HasForeignKey(c => c.ProductCategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

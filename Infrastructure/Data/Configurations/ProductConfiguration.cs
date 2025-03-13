using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class ProductConfiguration: IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(p => p.Description)
                   .HasMaxLength(500);

            builder.Property(p => p.Price)
                   .HasPrecision(18, 2);

            builder.HasOne(p => p.SubCategory)
                   .WithMany(sc => sc.Products)
                   .HasForeignKey(p => p.SubCategoryId);
        }
    }
}

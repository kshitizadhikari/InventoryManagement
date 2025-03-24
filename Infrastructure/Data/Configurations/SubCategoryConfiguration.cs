using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder.HasKey(sc => sc.Id);

            builder.Property(sc => sc.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasIndex(c => c.Name)
                   .IsUnique();

            builder.HasOne(sc => sc.Category)
                   .WithMany(c => c.SubCategories)
                   .HasForeignKey(sc => sc.CategoryId);

            builder.HasMany(sc => sc.Products)
                   .WithOne(p => p.SubCategory)
                   .HasForeignKey(p => p.SubCategoryId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

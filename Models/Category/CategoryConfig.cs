using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Models
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
                .ToTable("Category");

            builder
                .HasKey(c => c.ID);

            builder
                .Property(c => c.ID)
                .ValueGeneratedOnAdd();

            builder
                .Property(c => c.IsDeleted)
                .HasDefaultValue(false)
                .IsRequired();

            builder
                .Property(c => c.Name)
                .HasMaxLength(30)
                .IsRequired();

            builder
                .HasMany(c => c.Vendors)
                .WithOne(v => v.Category)
                .HasForeignKey(v => v.CategoryId);
        }
    }

}
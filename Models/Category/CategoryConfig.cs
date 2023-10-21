using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
                .Property(c => c.PicUrl)
                .IsRequired()
                .HasMaxLength(2083);

            builder
                .HasMany(c => c.Services)
                .WithOne(s => s.Category)
                .HasForeignKey(s => s.CategoryID);
        }
    }

}
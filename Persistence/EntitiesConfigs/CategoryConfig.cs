﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistence.EntitiesConfigs
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
                .ToTable("Category");

            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.Id)
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
                .HasForeignKey(v => v.CategoryId)
                .IsRequired();
        }
    }

}
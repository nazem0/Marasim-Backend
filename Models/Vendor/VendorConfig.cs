﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Reflection.Emit;


namespace Models
{
    public class VendorConfig : IEntityTypeConfiguration<Vendor>
    {
        public void Configure(EntityTypeBuilder<Vendor> builder)
        {
            builder
                .ToTable("Vendor");

            // ID
            builder
                .HasKey(v => v.ID);

            builder
                .Property(v => v.ID)
                .ValueGeneratedOnAdd();




            // Properties

            builder
                .Property(v => v.IsDeleted)
                .HasDefaultValue(false)
                .IsRequired();

            builder
              .Property(v => v.UserID)
              .IsRequired();


            builder
                .Property(v => v.Summary)
                .IsRequired().HasMaxLength(1000);




            builder
                 .HasMany(v => v.Followers)
                 .WithOne(f => f.Vendor)
                 .OnDelete(DeleteBehavior.NoAction)
                 .HasForeignKey(f => f.VendorID);

        }
    }
}
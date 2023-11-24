using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models
{
    public class WithdrawalConfig : IEntityTypeConfiguration<Withdrawal>
    {
        public void Configure(EntityTypeBuilder<Withdrawal> builder)
        {
            builder
                .ToTable("Withdrawal");

            // ID
            builder
                .HasKey(w => w.Id);

            builder
                .Property(w => w.Id)
                .ValueGeneratedOnAdd();

            // Properties

            builder
                .Property(w => w.DateTime)
                .IsRequired();

            builder
                .Property(w => w.InstaPay)
                .HasMaxLength(1000)
                .IsRequired();

            builder
               .Property(w => w.IsConfirmed)
               .HasDefaultValue(false)
               .IsRequired();

            builder
               .Property(w => w.VendorId)
               .IsRequired();

            
            builder
                .HasOne(w => w.Vendor)
                .WithMany(v => v.Withdrawals)
                .HasForeignKey(w => w.VendorId);

        }
    }
}


using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
namespace Models
{
public class BookingConfig : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
    builder
            .ToTable("Booking");

        builder
            .HasKey(b => b.ID);

        builder
            .Property(b => b.ID)
            .HasMaxLength(11)
            .ValueGeneratedOnAdd();

        builder
            .Property(b => b.UserID)
            .IsRequired();

        builder
            .Property(b => b.TotalPrice)
            .HasMaxLength(20)
            .IsRequired();

        builder
            .Property(b => b.DateTime)
            .HasDefaultValue(DateTime.Now);

        builder
            .HasMany(b => b.BookingsDetails)
            .WithOne(bd => bd.Booking)
            .HasForeignKey(bd => bd.BookingID)
            .IsRequired();

       
    }
}

}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
namespace Models
{
    public class BookingDetailsConfig : IEntityTypeConfiguration<BookingDetails>
    {
        public void Configure(EntityTypeBuilder<BookingDetails> builder)
        {
            builder
                .ToTable("BookingDetails");

            builder
                .HasKey(bd => bd.ID);

            builder
                .Property(bd => bd.ID)
                .ValueGeneratedOnAdd();

            builder
                .Property(bd => bd.BookingId)
                .HasMaxLength(11)
                .IsRequired();

            builder
                .Property(bd => bd.Status)
                .HasDefaultValue(Status.Pending)
                .IsRequired();

            builder
                .Property(bd => bd.ServiceId)
                .HasMaxLength(11)
                .IsRequired();

            builder
                .Property(bd => bd.DateTime)
                .HasDefaultValue(DateTime.Now);

            builder
                .Property(bd => bd.Code)
                .HasMaxLength(8);
        }
    }


}
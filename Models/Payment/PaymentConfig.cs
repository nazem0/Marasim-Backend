using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PaymentConfig : IEntityTypeConfiguration<Payment>
    {

        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payment");
            builder.HasKey(p => p.Id);
            builder
                .Property(p => p.Id)
                .HasMaxLength(11)
                .ValueGeneratedOnAdd();

            builder
                .Property(p => p.DateTime)
                .IsRequired()
                .HasDefaultValue(DateTime.Now);

            builder
                .Property(p => p.InstaPay)
                .HasMaxLength(1000)
                .IsRequired();

            builder
                .HasOne(p => p.Reservation)
                .WithOne(r => r.Payment)
                .HasForeignKey<Payment>(p => p.ReservationId);
        }
    }
}

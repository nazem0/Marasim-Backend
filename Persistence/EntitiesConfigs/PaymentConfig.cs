using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntitiesConfigs
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
                .IsRequired();

            builder
               .Property(p => p.IsWithdrawn)
               .HasDefaultValue(false)
               .IsRequired();

            builder
                .Property(p => p.Amount)
                .IsRequired();

            builder
                .Property(p => p.InstaPay)
                .HasMaxLength(1000)
                .IsRequired();

            builder
                .HasOne(p => p.Reservation)
                .WithOne(r => r.Payment)
                .HasForeignKey<Payment>(p => p.ReservationId);

            builder
                .HasOne(p => p.Withdrawal)
                .WithMany(w => w.Payments)
                .HasForeignKey(p => p.WithdrawalId)
                .IsRequired(false);
        }
    }
}

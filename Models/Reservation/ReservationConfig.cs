using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models
{
    public class ReservationConfig : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder
                .HasKey(r => r.Id);

            builder
                .Property(r => r.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(r => r.Price)
                .IsRequired();

            builder
                .Property(r => r.Status)
                .HasDefaultValue('p')
                .IsRequired();

            builder
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId)
                .IsRequired();

            builder
                .HasOne(r => r.Service)
                .WithMany(s => s.Reservations)
                .HasForeignKey(r => r.ServiceId)
                .IsRequired();


        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models
{
    public class ReservationConfig : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder
                .ToTable("Reservation");
            builder
                .HasKey(r => r.Id);

            builder
                .Property(r => r.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(r => r.Price)
                .IsRequired();

            builder
                .Property(r => r.IsDeleted)
                .HasDefaultValue(false)
                .IsRequired();

            builder
                .Property(r => r.DateTime)
                .IsRequired();

            builder
                .Property(r => r.Status)
                .HasDefaultValue('p')
                .IsRequired();

            builder
                .Property(r => r.Latitude)
                .IsRequired()
                .HasColumnType("decimal")
                .HasPrecision(18, 15);

            builder
                .Property(r => r.Longitude)
                .IsRequired()
                .HasColumnType("decimal")
                .HasPrecision(18, 15);

            builder
                .Property(r => r.Address)
                .IsRequired();

            builder
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId)
                .IsRequired();
        }
    }
}

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
                .Property(r => r.CityId)
                .IsRequired();
            builder
                .Property(r => r.GovernorateId)
                .IsRequired();

            builder
                .Property(r => r.District)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(r => r.Street)
                .HasMaxLength(100);

            builder
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId)
                .IsRequired();

            builder
                .HasOne(r => r.City)
                .WithMany(c => c.Reservations)
                .HasForeignKey(r => r.CityId);

        }
    }
}

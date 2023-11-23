using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Models
{
    public class ReviewConfig : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder
                .ToTable("Review");

            builder
                .HasKey(r => r.Id);

            builder
                .Property(r => r.UserId)
                .IsRequired();

            builder
                .Property(r => r.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(r => r.ServiceId)
                .IsRequired();

            builder
                .Property(r => r.Rate)
                .IsRequired();

            builder
                .Property(r => r.DateTime)
                .IsRequired();

            builder
                .Property(r => r.Message)
                .HasMaxLength(1000);

            builder
                .HasOne(r => r.Reservation)
                .WithOne(r => r.Review)
                .HasForeignKey<Review>(r => r.ReservationId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
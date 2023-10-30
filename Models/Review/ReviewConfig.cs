using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;


namespace Models
{
    public class ReviewConfig : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder
                .ToTable("Review");

            builder
                .HasKey(r => r.ID);

            builder
                .Property(r => r.UserID)
                .IsRequired();

            builder
                .Property(r => r.ID)
                .ValueGeneratedOnAdd();

            builder
                .Property(r => r.ServiceID)
                .IsRequired();

            builder
                .Property(r => r.Rate)
                .IsRequired();

            builder
                .Property(r => r.DateTime)
                .HasDefaultValue(DateTime.Now)
                .IsRequired();

            builder
                .Property(r => r.Message)
                .IsRequired()
                .HasMaxLength(1000);
        }
    }
}
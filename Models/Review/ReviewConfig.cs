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
                .HasDefaultValue(DateTime.Now)
                .IsRequired();

            builder
                .Property(r => r.Message)
                .IsRequired()
                .HasMaxLength(1000);
        }
    }
}
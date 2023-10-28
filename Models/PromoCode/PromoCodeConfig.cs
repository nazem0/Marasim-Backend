using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;


namespace Models
{
    public class PromoCodeConfig : IEntityTypeConfiguration<PromoCode>
    {
        public void Configure(EntityTypeBuilder<PromoCode> builder)
        {
            builder.ToTable("PromoCode");

            //ID

            builder
                .HasKey(pc => pc.ID);

            builder
                .Property(pc => pc.ID)
                .ValueGeneratedOnAdd()
                .HasMaxLength(11);

            //Propertes

            builder
                .Property(pc => pc.Code)
                .HasMaxLength(8)
                .IsRequired();

            builder
                .Property(pc => pc.IsDeleted)
                .HasDefaultValue(false)
                .IsRequired();

            builder
                .Property(pc => pc.StartDate)
                .HasDefaultValue(DateTime.Now);

            builder
                .Property(pc => pc.ExpirationDate)
                .IsRequired();

            builder
                .Property(pc => pc.Limit);

            //relationships

            builder
                .HasOne(pc => pc.Service)
                .WithOne(s => s.PromoCode)
                .HasForeignKey<PromoCode>(pc => pc.ServiceID)
                .IsRequired();



        }
    }
}
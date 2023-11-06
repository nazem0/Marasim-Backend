using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models
{
    public class ServiceConfig : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.ToTable("Service");
            builder
                .HasKey(s => s.Id);

            builder
                .Property(s => s.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(s => s.Title)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(s => s.Description)
                .HasMaxLength(1000)
                .IsRequired();

            builder
                .Property(s => s.IsDeleted)
                .HasDefaultValue(false)
                .IsRequired();

            builder
                .Property(s => s.Price)
                .IsRequired();


            //relationship
            builder
               .HasMany(s => s.Reviews)
               .WithOne(r => r.Service)
               .HasForeignKey(r => r.ServiceId)
               .OnDelete(DeleteBehavior.ClientCascade)
               .IsRequired();

            builder
                .HasOne(s => s.Vendor)
                .WithMany(v => v.Services)
                .HasForeignKey(s => s.VendorID)
                .IsRequired();

            builder
               .HasOne(s => s.PromoCode)
               .WithOne(pc => pc.Service)
               .HasForeignKey<PromoCode>(pc => pc.ServiceId)
               .IsRequired();

            builder
                .HasMany(s => s.Reservations)
                .WithOne(r => r.Service)
                .HasForeignKey(r => r.ServiceId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }

}
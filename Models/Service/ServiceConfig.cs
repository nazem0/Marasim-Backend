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
                .HasKey(s => s.ID);

            builder
                .Property(s => s.ID)
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

            //builder
            //    .Property(s=> s.Street)
            //    .HasMaxLength(100);

            //builder
            //    .Property(s=> s.District)
            //    .HasMaxLength(100)
            //    .IsRequired();

            //builder
            //    .Property(s=> s.Governance)
            //    .HasMaxLength(100)
            //    .IsRequired();


            //relationship
            builder
               .HasMany(s => s.Reviews)
               .WithOne(r => r.Service)
               .HasForeignKey(r => r.ServiceID)
               .OnDelete(DeleteBehavior.ClientCascade)
               .IsRequired();

            builder
                .HasOne(s => s.Vendor)
                .WithMany(v => v.Services)
                .HasForeignKey(s => s.VendorID)
                .IsRequired();

            builder
               .HasMany(s => s.PromoCodes)
               .WithOne(pc => pc.Service)
               .HasForeignKey(pc => pc.ServiceID)
               .IsRequired();

            //builder
            //    .HasOne(s=> s.Category)
            //    .WithMany(c=> c.Services)
            //    .HasForeignKey(s=> s.CategoryID)
            //    .IsRequired();

            builder
                .HasMany(s => s.BookingDetails)
                .WithOne(bd => bd.Service)
                .HasForeignKey(bd => bd.ServiceID)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }

}
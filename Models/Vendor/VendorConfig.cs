using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Models
{
    public class VendorConfig : IEntityTypeConfiguration<Vendor>
    {
        public void Configure(EntityTypeBuilder<Vendor> builder)
        {
            builder
                .ToTable("Vendor");

            // ID
            builder
                .HasKey(v => v.Id);

            builder
                .Property(v => v.Id)
                .ValueGeneratedOnAdd();




            // Properties

            builder
                .Property(v => v.IsDeleted)
                .HasDefaultValue(false)
                .IsRequired();

            builder
              .Property(v => v.UserId)
              .IsRequired();

            builder
                .Property(v => v.CityId)
                .IsRequired();

            builder
                .Property(v => v.GovernorateId)
                .IsRequired();

            builder
                .Property(v => v.Summary)
                .IsRequired().HasMaxLength(1000);

            builder
                .Property(v => v.Latitude)
                .HasColumnType("decimal")
                .HasPrecision(18, 15);

            builder
                .Property(v => v.Longitude)
                .HasColumnType("decimal")
                .HasPrecision(18, 15);

            builder
                .Property(v => v.District)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(v => v.Street)
                .HasMaxLength(100);

            builder
                .Property(v => v.ExternalUrl)
                .HasMaxLength(2085);

            builder
                 .HasMany(v => v.Followers)
                 .WithOne(f => f.Vendor)
                 .OnDelete(DeleteBehavior.NoAction)
                 .HasForeignKey(f => f.VendorId);

            builder
                .HasOne(v => v.City)
                .WithMany(c => c.Vendors)
                .HasForeignKey(v => v.CityId);

            builder
                .HasOne(v => v.Governorate)
                .WithMany(g => g.Vendors)
                .HasForeignKey(v => v.GovernorateId);

        }
    }
}
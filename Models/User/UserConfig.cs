using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models
{

    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .ToTable("User");

            // Properties

            builder
                .Property(u => u.IsDeleted)
                .HasDefaultValue(false)
                .IsRequired();

            builder
                .Property(u => u.Name)
                .IsRequired().HasMaxLength(150);

            builder
             .Property(u => u.Email)
             .IsRequired().HasMaxLength(320);

            builder
            .Property(u => u.PhoneNumber)
            .IsRequired().HasMaxLength(15);

            builder
            .Property(u => u.PicUrl)
            .HasMaxLength(2083)
            .IsRequired();

            builder
            .Property(u => u.Gender)
            .IsRequired();

            builder
            .Property(u => u.RegistrationDate)
            .IsRequired();

            builder
            .Property(u => u.NationalId)
            .HasMaxLength(20)
            .IsRequired();
            //unique
            // Relations

            builder
               .HasMany(u => u.Reservations)
               .WithOne(b => b.User)
               .HasForeignKey(b => b.UserId)
               .IsRequired();

            builder
                .HasMany(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            builder
                .HasMany(f => f.Follows)
                .WithOne(u => u.User)
                .HasForeignKey(f => f.UserId)
                .IsRequired();

            builder
                .HasMany(u => u.Reviews)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(v => v.Vendor)
                .WithOne(u => u.User)
                .HasForeignKey<Vendor>(v => v.UserId);

            builder
                .HasMany(u => u.Reacts)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId);
        }
    }
}
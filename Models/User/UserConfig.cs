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
                .IsRequired().HasMaxLength(20);

            builder
             .Property(u => u.Email)
             .IsRequired().HasMaxLength(50);

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
            .Property(u => u.NationalID)
            .HasMaxLength(20)
            .IsRequired();
            //unique
            // Relations

            builder
               .HasMany(u => u.Bookings)
               .WithOne(b => b.User)
               .HasForeignKey(b => b.UserID)
               .IsRequired();

            builder
              .HasOne(u => u.CheckList)
              .WithOne(cl => cl.User)
              .HasForeignKey<CheckList>(cl => cl.UserID);



            builder
                .HasMany(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserID)
                .OnDelete(DeleteBehavior.Cascade);


            builder
                .HasMany(f => f.Follows)
                .WithOne(u => u.User)
                .HasForeignKey(f => f.UserID)
                .IsRequired();

            builder
                .HasMany(u => u.Reviews)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(v => v.Vendor)
                .WithOne(u => u.User)
                .HasForeignKey<Vendor>(v => v.UserID);

            builder
                .HasMany(u => u.Reacts)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserID);
        }
    }
}
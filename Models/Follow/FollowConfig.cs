using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models
{
    public class FollowConfig : IEntityTypeConfiguration<Follow>
    {
        public void Configure(EntityTypeBuilder<Follow> builder)
        {
            builder
                .ToTable("Follow");
            builder
                .HasKey(f => f.Id);
            ;

            builder
                .Property(f => f.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(c => c.UserId)
                .IsRequired();

            builder
                .Property(f => f.VendorId)
                .IsRequired();

            builder
                .Property(f => f.DateTime)
                .IsRequired();




        }
    }

}
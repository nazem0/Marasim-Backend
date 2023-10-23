using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models
{
    public class ReactConfig : IEntityTypeConfiguration<React>
    {
        public void Configure(EntityTypeBuilder<React> builder)
        {
            builder
                .ToTable("React");
            builder
                .HasKey(r => r.ID);

            //Properties
            builder
                .Property(r => r.ID)
                .ValueGeneratedOnAdd()
                .HasMaxLength(11);

            builder
                .Property(r => r.PostID)
                .HasMaxLength(11)
                .IsRequired();

            builder
                .Property(r => r.UserID)
                .IsRequired();


            builder
                .Property(r => r.DateTime)
                .IsRequired();
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Models
{
    public class WeddingInvitationConfig : IEntityTypeConfiguration<WeddingInvitation>
    {
        public void Configure(EntityTypeBuilder<WeddingInvitation> builder)
        {
            builder
                .ToTable("WeddingInvitation");

            // ID
            builder
                .HasKey(w => w.Id);

            builder
                .Property(w => w.Id)
                .ValueGeneratedOnAdd();




            // Properties

            builder
              .Property(w => w.UserId)
              .IsRequired();


            builder
                .Property(w => w.Decsription)
                .IsRequired().HasMaxLength(1000);

            builder
               .Property(w => w.GroomName)
               .IsRequired().HasMaxLength(20);

            builder
               .Property(w => w.GroomPicUrl)
               .IsRequired().HasMaxLength(2083);

            builder
               .Property(w => w.BrideName)
               .IsRequired().HasMaxLength(20);

            builder
               .Property(w => w.BridePicUrl)
               .IsRequired().HasMaxLength(2083);

            builder
               .Property(w => w.PosterUrl)
               .IsRequired().HasMaxLength(2083);

            builder
               .Property(w => w.Date)
               .IsRequired();

            builder
                .HasOne(wi => wi.User)
                .WithMany(u => u.WeddingInvitations)
                .HasForeignKey(wi => wi.UserId);


        }
    }
}
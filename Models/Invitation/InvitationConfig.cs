using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Models
{
    public class InvitationConfig : IEntityTypeConfiguration<Invitation>
    {
        public void Configure(EntityTypeBuilder<Invitation> builder)
        {
            builder
                .ToTable("Invitation");

            // ID
            builder
                .HasKey(w => w.Id);

            builder
                .Property(w => w.Id)
                .ValueGeneratedOnAdd();




            // Properties


            builder
                .Property(w => w.Location)
                .IsRequired().HasMaxLength(150);

            builder
               .Property(w => w.GroomName)
               .IsRequired().HasMaxLength(150);

            builder
               .Property(w => w.GroomPicUrl)
               .IsRequired().HasMaxLength(2083);

            builder
               .Property(w => w.BrideName)
               .IsRequired().HasMaxLength(150);

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
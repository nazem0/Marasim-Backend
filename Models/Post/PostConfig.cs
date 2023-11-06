using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models
{
    public class PostConfig : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Post");

            builder.HasKey(p => p.Id);

            //Properties
            builder
                .Property(p => p.Id)
                .HasMaxLength(11)
                .ValueGeneratedOnAdd();

            builder
                .Property(p => p.VendorId)
                .HasMaxLength(11)
                .IsRequired();
            builder
                .Property(p => p.Title)
                .HasMaxLength(100)
                .IsRequired();
            builder
                .Property(p => p.Description)
                .HasMaxLength(1000)
                .IsRequired();
            builder
                .Property(p => p.DateTime)
                .IsRequired();

            // Realeations
            //builder
            //    .HasMany(p => p.PostAttachments)
            //    .WithOne(pa => pa.Post)
            //    .HasForeignKey(pa => pa.PostID)
            //    .IsRequired();

            builder
                .HasMany(p => p.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder
                .HasMany(p => p.Reacts)
                .WithOne(r => r.Post)
                .HasForeignKey(r => r.PostId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
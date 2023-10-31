using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models
{
    public class PostAttachmentConfig : IEntityTypeConfiguration<PostAttachment>
    {
        public void Configure(EntityTypeBuilder<PostAttachment> builder)
        {
            builder.ToTable("PostAttachment");

            builder.HasKey(pa => pa.ID);

            builder
                .Property(pa => pa.ID)
                .ValueGeneratedOnAdd()
                .HasMaxLength(11);
            builder
                .Property(pa => pa.PostID)
                .HasMaxLength(11)
                .IsRequired();
            builder
                .Property(pa => pa.AttachmentUrl)
                .HasMaxLength(2085)
                .IsRequired();

            builder
               .HasOne(pa => pa.Post)
               .WithMany(pa => pa.PostAttachments)
               .HasForeignKey(pa => pa.PostID);

        }
    }
}
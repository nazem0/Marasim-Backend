using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace Models {
    public class CommentConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder
                .ToTable("Comment");
            builder
                .HasKey(c => c.ID);

            builder
                .Property(c => c.ID)
                .HasMaxLength(11)
                .ValueGeneratedOnAdd();

            builder
                .Property(c => c.PostID)
                .HasMaxLength(11)
                .IsRequired();

            builder
                .Property(c => c.UserID)
                .IsRequired();

            builder
                .Property(c => c.Text)
                .HasMaxLength(1000)
                .IsRequired();
            builder
                .Property(c => c.DateTime)
                .IsRequired();


        }
    }
}
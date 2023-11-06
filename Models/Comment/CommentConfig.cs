using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Models
{
    public class CommentConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder
                .ToTable("Comment");
            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.Id)
                .HasMaxLength(11)
                .ValueGeneratedOnAdd();

            builder
                .Property(c => c.PostId)
                .HasMaxLength(11)
                .IsRequired();

            builder
                .Property(c => c.UserId)
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
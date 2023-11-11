using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models
{
    public class CheckListItemConfig : IEntityTypeConfiguration<CheckListItem>
    {
        public void Configure(EntityTypeBuilder<CheckListItem> builder)
        {
            builder
                .ToTable("CheckListItem");

            builder
                .HasKey(cli => cli.Id);

            builder
                .Property(cli => cli.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(cli => cli.CheckListId)
                .IsRequired();

            builder
                .Property(cli => cli.Text)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(cli => cli.Status)
                .HasDefaultValue(false)
                .IsRequired();

        }
    }

}


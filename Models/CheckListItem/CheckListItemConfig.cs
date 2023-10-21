using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
public class CheckListItemConfig : IEntityTypeConfiguration<CheckListItem>
{
    public void Configure(EntityTypeBuilder<CheckListItem> builder)
    {
        builder
            .ToTable("CheckListItem");

        builder
            .HasKey(cli => cli.ID);

        builder
            .Property(cli => cli.ID)
            .ValueGeneratedOnAdd();

        builder
            .Property(cli => cli.ChecklistID)
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


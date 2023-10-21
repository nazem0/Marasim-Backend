using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Models
{
    public class CheckListConfig : IEntityTypeConfiguration<CheckList>
    {
        public void Configure(EntityTypeBuilder<CheckList> builder)
        {
            builder
                .ToTable("CheckList");

            builder
                .HasKey(cl => cl.ID);

            builder
                .Property(cl => cl.ID)
                .ValueGeneratedOnAdd();

            builder
                .Property(cl => cl.UserID)
                .IsRequired();

            builder
               .Property(cl => cl.WeddingDate)
               .IsRequired();

            builder
                .HasMany(cl => cl.CheckListItems)
                .WithOne(cli => cli.CheckList)
                .HasForeignKey(cli => cli.ChecklistID);
        }
    }


}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Models
{
    public class CheckListConfig : IEntityTypeConfiguration<CheckList>
    {
        public void Configure(EntityTypeBuilder<CheckList> builder)
        {
            builder
                .ToTable("CheckList");

            builder
                .HasKey(cl => cl.Id);

            builder
                .Property(cl => cl.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(cl => cl.UserId)
                .IsRequired();

            builder
               .Property(cl => cl.WeddingDate)
               .IsRequired();

            builder
                .HasMany(cl => cl.CheckListItems)
                .WithOne(cli => cli.CheckList)
                .HasForeignKey(cli => cli.ChecklistId);
        }
    }


}
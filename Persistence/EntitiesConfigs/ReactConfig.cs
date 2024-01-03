using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntitiesConfigs
{
    public class ReactConfig : IEntityTypeConfiguration<React>
    {
        public void Configure(EntityTypeBuilder<React> builder)
        {
            builder
                .ToTable("React");
            builder
                .HasKey(r => r.Id);

            //Properties
            builder
                .Property(r => r.Id)
                .ValueGeneratedOnAdd()
                .HasMaxLength(11);

            builder
                .Property(r => r.PostId)
                .HasMaxLength(11)
                .IsRequired();

            builder
                .Property(r => r.UserId)
                .IsRequired();


            builder
                .Property(r => r.DateTime)
                .IsRequired();
        }
    }
}
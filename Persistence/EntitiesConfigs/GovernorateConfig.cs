using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntitiesConfigs
{
    public class GovernorateConfig : IEntityTypeConfiguration<Governorate>
    {
        public void Configure(EntityTypeBuilder<Governorate> builder)
        {
            builder.ToTable("Governorate");

            builder.HasKey(g => g.Id);


            builder.Property(g => g.NameAr)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(g => g.NameEn)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}

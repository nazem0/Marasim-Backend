using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
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

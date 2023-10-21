using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class FollowConfig : IEntityTypeConfiguration<Follow>
    {
        public void Configure(EntityTypeBuilder<Follow> builder)
        {
            builder
                .ToTable("Follow");
            builder
                .HasKey(f => f.ID);
            ;

            builder
                .Property(f => f.ID)
                .ValueGeneratedOnAdd();

            builder
                .Property(c => c.UserID)
                .IsRequired();

            builder
                .Property(f => f.VendorID)
                .IsRequired();

            builder
                .Property(f => f.DateTime)
                .IsRequired();




        }
    }

}
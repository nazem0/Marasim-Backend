using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Models
{
    public class ServiceAttachmentConfig : IEntityTypeConfiguration<ServiceAttachment>
    {
        public void Configure(EntityTypeBuilder<ServiceAttachment> builder)
        {
            builder
                .ToTable("ServiceAttachment");

            builder
                .HasKey(i => i.Id);

            builder
                .Property(i => i.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(i => i.AttachmentUrl)
                .IsRequired()
                .HasMaxLength(1000);


            //relationship
            builder
                .HasOne(i => i.Service)
                .WithMany(s => s.ServiceAttachments)
                .HasForeignKey(i => i.ServiceID);


        }

    }
}
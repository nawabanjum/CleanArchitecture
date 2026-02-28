using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Entities;

namespace Portfolio.Infrastructure.Data.Configurations;

public class CertificationConfiguration : IEntityTypeConfiguration<Certification>
{
    public void Configure(EntityTypeBuilder<Certification> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.IssuingOrganization)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(c => c.CredentialId)
            .HasMaxLength(200);

        builder.Property(c => c.CredentialUrl)
            .HasMaxLength(500);
    }
}

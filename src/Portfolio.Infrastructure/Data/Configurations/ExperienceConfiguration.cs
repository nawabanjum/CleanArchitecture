using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Entities;

namespace Portfolio.Infrastructure.Data.Configurations;

public class ExperienceConfiguration : IEntityTypeConfiguration<Experience>
{
    public void Configure(EntityTypeBuilder<Experience> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.JobTitle)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Company)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Location)
            .HasMaxLength(200);

        builder.Property(e => e.Description)
            .HasMaxLength(2000);
    }
}

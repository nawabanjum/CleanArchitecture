using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Entities;

namespace Portfolio.Infrastructure.Data.Configurations;

public class EducationConfiguration : IEntityTypeConfiguration<Education>
{
    public void Configure(EntityTypeBuilder<Education> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Degree)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.FieldOfStudy)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Institution)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(e => e.Description)
            .HasMaxLength(2000);
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Entities;

namespace Portfolio.Infrastructure.Data.Configurations;

public class SocialLinkConfiguration : IEntityTypeConfiguration<SocialLink>
{
    public void Configure(EntityTypeBuilder<SocialLink> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Url)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(s => s.Platform)
            .HasConversion<string>()
            .HasMaxLength(20);
    }
}

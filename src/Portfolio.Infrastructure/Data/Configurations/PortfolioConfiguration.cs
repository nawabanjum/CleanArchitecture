using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Portfolio.Infrastructure.Data.Configurations;

public class PortfolioConfiguration : IEntityTypeConfiguration<Domain.Entities.Portfolio>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Portfolio> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Slug)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasIndex(p => p.Slug)
            .IsUnique();

        builder.Property(p => p.Summary)
            .HasMaxLength(1000);

        builder.Property(p => p.UserId)
            .IsRequired();

        builder.HasIndex(p => p.UserId);

        builder.HasMany(p => p.Educations)
            .WithOne(e => e.Portfolio)
            .HasForeignKey(e => e.PortfolioId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Experiences)
            .WithOne(e => e.Portfolio)
            .HasForeignKey(e => e.PortfolioId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Projects)
            .WithOne(e => e.Portfolio)
            .HasForeignKey(e => e.PortfolioId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Skills)
            .WithOne(e => e.Portfolio)
            .HasForeignKey(e => e.PortfolioId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Certifications)
            .WithOne(e => e.Portfolio)
            .HasForeignKey(e => e.PortfolioId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.SocialLinks)
            .WithOne(e => e.Portfolio)
            .HasForeignKey(e => e.PortfolioId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

using Portfolio.Domain.Entities.Common;

namespace Portfolio.Domain.Entities;

public class Certification : BaseEntity
{
    public Guid PortfolioId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string IssuingOrganization { get; set; } = string.Empty;
    public DateTime IssueDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public string? CredentialId { get; set; }
    public string? CredentialUrl { get; set; }

    public Portfolio Portfolio { get; set; } = null!;
}

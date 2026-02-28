using Portfolio.Domain.Entities.Common;

namespace Portfolio.Domain.Entities;

public class Education : BaseEntity
{
    public Guid PortfolioId { get; set; }
    public string Degree { get; set; } = string.Empty;
    public string FieldOfStudy { get; set; } = string.Empty;
    public string Institution { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Description { get; set; }

    public Portfolio Portfolio { get; set; } = null!;
}

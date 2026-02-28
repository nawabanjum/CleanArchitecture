using Portfolio.Domain.Entities.Common;

namespace Portfolio.Domain.Entities;

public class Experience : BaseEntity
{
    public Guid PortfolioId { get; set; }
    public string JobTitle { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public string? Location { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsCurrent { get; set; }
    public string? Description { get; set; }

    public Portfolio Portfolio { get; set; } = null!;
}

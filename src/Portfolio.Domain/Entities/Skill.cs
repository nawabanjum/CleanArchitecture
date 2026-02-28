using Portfolio.Domain.Entities.Common;
using Portfolio.Domain.Enums;

namespace Portfolio.Domain.Entities;

public class Skill : BaseEntity
{
    public Guid PortfolioId { get; set; }
    public string Name { get; set; } = string.Empty;
    public ProficiencyLevel ProficiencyLevel { get; set; }
    public string? Category { get; set; }

    public Portfolio Portfolio { get; set; } = null!;
}

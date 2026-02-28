using Portfolio.Domain.Entities.Common;

namespace Portfolio.Domain.Entities;

public class Project : BaseEntity
{
    public Guid PortfolioId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public List<string> TechStack { get; set; } = new();
    public string? LiveUrl { get; set; }
    public string? GithubUrl { get; set; }
    public string? ImageUrl { get; set; }

    public Portfolio Portfolio { get; set; } = null!;
}

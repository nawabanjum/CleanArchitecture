using Portfolio.Domain.Entities.Common;
using Portfolio.Domain.Enums;

namespace Portfolio.Domain.Entities;

public class SocialLink : BaseEntity
{
    public Guid PortfolioId { get; set; }
    public SocialPlatform Platform { get; set; }
    public string Url { get; set; } = string.Empty;

    public Portfolio Portfolio { get; set; } = null!;
}

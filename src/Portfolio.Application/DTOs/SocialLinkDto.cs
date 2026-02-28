using Portfolio.Domain.Enums;

namespace Portfolio.Application.DTOs;

public class SocialLinkDto
{
    public Guid Id { get; set; }
    public SocialPlatform Platform { get; set; }
    public string Url { get; set; } = string.Empty;
}

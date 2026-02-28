using MediatR;
using Portfolio.Application.DTOs;
using Portfolio.Domain.Enums;

namespace Portfolio.Application.Features.SocialLinks.Commands.Create;

public class CreateSocialLinkCommand : IRequest<SocialLinkDto>
{
    public Guid PortfolioId { get; set; }
    public SocialPlatform Platform { get; set; }
    public string Url { get; set; } = string.Empty;
}

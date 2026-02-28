using MediatR;
using Portfolio.Application.DTOs;
using Portfolio.Domain.Enums;

namespace Portfolio.Application.Features.SocialLinks.Commands.Update;

public class UpdateSocialLinkCommand : IRequest<SocialLinkDto>
{
    public Guid Id { get; set; }
    public Guid PortfolioId { get; set; }
    public SocialPlatform Platform { get; set; }
    public string Url { get; set; } = string.Empty;
}

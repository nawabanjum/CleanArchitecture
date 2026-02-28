using MediatR;

namespace Portfolio.Application.Features.SocialLinks.Commands.Delete;

public class DeleteSocialLinkCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public Guid PortfolioId { get; set; }
}

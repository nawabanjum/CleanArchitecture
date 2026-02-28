using FluentValidation;

namespace Portfolio.Application.Features.SocialLinks.Commands.Create;

public class CreateSocialLinkCommandValidator : AbstractValidator<CreateSocialLinkCommand>
{
    public CreateSocialLinkCommandValidator()
    {
        RuleFor(x => x.PortfolioId).NotEmpty();
        RuleFor(x => x.Url).NotEmpty().MaximumLength(500);
    }
}

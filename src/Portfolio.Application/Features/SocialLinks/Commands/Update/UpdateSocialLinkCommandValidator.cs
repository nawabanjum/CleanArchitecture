using FluentValidation;

namespace Portfolio.Application.Features.SocialLinks.Commands.Update;

public class UpdateSocialLinkCommandValidator : AbstractValidator<UpdateSocialLinkCommand>
{
    public UpdateSocialLinkCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.PortfolioId).NotEmpty();
        RuleFor(x => x.Url).NotEmpty().MaximumLength(500);
    }
}

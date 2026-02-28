using FluentValidation;

namespace Portfolio.Application.Features.Certifications.Commands.Create;

public class CreateCertificationCommandValidator : AbstractValidator<CreateCertificationCommand>
{
    public CreateCertificationCommandValidator()
    {
        RuleFor(x => x.PortfolioId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.IssuingOrganization).NotEmpty().MaximumLength(300);
        RuleFor(x => x.IssueDate).NotEmpty();
    }
}

using FluentValidation;

namespace Portfolio.Application.Features.Certifications.Commands.Update;

public class UpdateCertificationCommandValidator : AbstractValidator<UpdateCertificationCommand>
{
    public UpdateCertificationCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.PortfolioId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.IssuingOrganization).NotEmpty().MaximumLength(300);
        RuleFor(x => x.IssueDate).NotEmpty();
    }
}

using FluentValidation;

namespace Portfolio.Application.Features.Educations.Commands.Update;

public class UpdateEducationCommandValidator : AbstractValidator<UpdateEducationCommand>
{
    public UpdateEducationCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.PortfolioId).NotEmpty();
        RuleFor(x => x.Degree).NotEmpty().MaximumLength(200);
        RuleFor(x => x.FieldOfStudy).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Institution).NotEmpty().MaximumLength(300);
        RuleFor(x => x.StartDate).NotEmpty();
    }
}

using FluentValidation;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.Features.Portfolios.Commands.Create;

public class CreatePortfolioCommandValidator : AbstractValidator<CreatePortfolioCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreatePortfolioCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200);

        RuleFor(x => x.Slug)
            .NotEmpty().WithMessage("Slug is required.")
            .MaximumLength(200)
            .Matches("^[a-z0-9]+(?:-[a-z0-9]+)*$").WithMessage("Slug must be lowercase with hyphens only.")
            .MustAsync(BeUniqueSlug).WithMessage("This slug is already taken.");

        RuleFor(x => x.Summary)
            .MaximumLength(1000);
    }

    private async Task<bool> BeUniqueSlug(string slug, CancellationToken cancellationToken)
    {
        return !await _unitOfWork.Portfolios.SlugExistsAsync(slug);
    }
}

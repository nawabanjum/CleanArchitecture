using MediatR;

namespace Portfolio.Application.Features.Experiences.Commands.Delete;

public class DeleteExperienceCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public Guid PortfolioId { get; set; }
}

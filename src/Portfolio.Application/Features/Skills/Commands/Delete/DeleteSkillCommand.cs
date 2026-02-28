using MediatR;

namespace Portfolio.Application.Features.Skills.Commands.Delete;

public class DeleteSkillCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public Guid PortfolioId { get; set; }
}

using MediatR;

namespace Portfolio.Application.Features.Projects.Commands.Delete;

public class DeleteProjectCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public Guid PortfolioId { get; set; }
}

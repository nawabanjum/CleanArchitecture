using MediatR;

namespace Portfolio.Application.Features.Educations.Commands.Delete;

public class DeleteEducationCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public Guid PortfolioId { get; set; }
}

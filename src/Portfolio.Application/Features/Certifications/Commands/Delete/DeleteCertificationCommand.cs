using MediatR;

namespace Portfolio.Application.Features.Certifications.Commands.Delete;

public class DeleteCertificationCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public Guid PortfolioId { get; set; }
}

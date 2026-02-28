using MediatR;

namespace Portfolio.Application.Features.Portfolios.Commands.Delete;

public class DeletePortfolioCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}

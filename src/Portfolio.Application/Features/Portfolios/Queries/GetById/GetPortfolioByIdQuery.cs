using MediatR;
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Features.Portfolios.Queries.GetById;

public class GetPortfolioByIdQuery : IRequest<PortfolioDto>
{
    public Guid Id { get; set; }
}

using MediatR;
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Features.Portfolios.Queries.GetBySlug;

public class GetPortfolioBySlugQuery : IRequest<PortfolioDto>
{
    public string Slug { get; set; } = string.Empty;
}

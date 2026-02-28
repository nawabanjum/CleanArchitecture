using MediatR;
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Features.Portfolios.Queries.GetMyPortfolios;

public class GetMyPortfoliosQuery : IRequest<List<PortfolioDto>>
{
}

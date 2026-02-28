using MediatR;
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Features.Portfolios.Commands.Create;

public class CreatePortfolioCommand : IRequest<PortfolioDto>
{
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Summary { get; set; }
    public bool IsPublic { get; set; }
}

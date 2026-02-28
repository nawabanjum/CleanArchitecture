using MediatR;
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Features.Portfolios.Commands.Update;

public class UpdatePortfolioCommand : IRequest<PortfolioDto>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Summary { get; set; }
    public bool IsPublic { get; set; }
}

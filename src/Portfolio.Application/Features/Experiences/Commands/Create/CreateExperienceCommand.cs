using MediatR;
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Features.Experiences.Commands.Create;

public class CreateExperienceCommand : IRequest<ExperienceDto>
{
    public Guid PortfolioId { get; set; }
    public string JobTitle { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public string? Location { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsCurrent { get; set; }
    public string? Description { get; set; }
}

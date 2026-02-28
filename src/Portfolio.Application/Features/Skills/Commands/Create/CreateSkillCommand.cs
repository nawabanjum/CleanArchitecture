using MediatR;
using Portfolio.Application.DTOs;
using Portfolio.Domain.Enums;

namespace Portfolio.Application.Features.Skills.Commands.Create;

public class CreateSkillCommand : IRequest<SkillDto>
{
    public Guid PortfolioId { get; set; }
    public string Name { get; set; } = string.Empty;
    public ProficiencyLevel ProficiencyLevel { get; set; }
    public string? Category { get; set; }
}

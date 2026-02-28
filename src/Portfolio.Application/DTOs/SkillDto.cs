using Portfolio.Domain.Enums;

namespace Portfolio.Application.DTOs;

public class SkillDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ProficiencyLevel ProficiencyLevel { get; set; }
    public string? Category { get; set; }
}

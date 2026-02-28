namespace Portfolio.Application.DTOs;

public class PortfolioDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Summary { get; set; }
    public bool IsPublic { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public List<EducationDto> Educations { get; set; } = new();
    public List<ExperienceDto> Experiences { get; set; } = new();
    public List<ProjectDto> Projects { get; set; } = new();
    public List<SkillDto> Skills { get; set; } = new();
    public List<CertificationDto> Certifications { get; set; } = new();
    public List<SocialLinkDto> SocialLinks { get; set; } = new();
}

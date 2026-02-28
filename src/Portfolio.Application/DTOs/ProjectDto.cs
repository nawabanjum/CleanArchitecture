namespace Portfolio.Application.DTOs;

public class ProjectDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public List<string> TechStack { get; set; } = new();
    public string? LiveUrl { get; set; }
    public string? GithubUrl { get; set; }
    public string? ImageUrl { get; set; }
}

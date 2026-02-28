using MediatR;
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Features.Projects.Commands.Update;

public class UpdateProjectCommand : IRequest<ProjectDto>
{
    public Guid Id { get; set; }
    public Guid PortfolioId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public List<string> TechStack { get; set; } = new();
    public string? LiveUrl { get; set; }
    public string? GithubUrl { get; set; }
    public string? ImageUrl { get; set; }
}

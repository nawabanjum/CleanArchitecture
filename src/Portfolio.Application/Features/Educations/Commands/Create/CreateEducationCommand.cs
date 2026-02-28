using MediatR;
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Features.Educations.Commands.Create;

public class CreateEducationCommand : IRequest<EducationDto>
{
    public Guid PortfolioId { get; set; }
    public string Degree { get; set; } = string.Empty;
    public string FieldOfStudy { get; set; } = string.Empty;
    public string Institution { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Description { get; set; }
}

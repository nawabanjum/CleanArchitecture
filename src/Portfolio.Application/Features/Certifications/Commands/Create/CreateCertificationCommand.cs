using MediatR;
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Features.Certifications.Commands.Create;

public class CreateCertificationCommand : IRequest<CertificationDto>
{
    public Guid PortfolioId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string IssuingOrganization { get; set; } = string.Empty;
    public DateTime IssueDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public string? CredentialId { get; set; }
    public string? CredentialUrl { get; set; }
}

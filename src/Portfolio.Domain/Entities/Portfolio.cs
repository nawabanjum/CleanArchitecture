using Portfolio.Domain.Entities.Common;

namespace Portfolio.Domain.Entities;

public class Portfolio : BaseEntity
{
    public string UserId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Summary { get; set; }
    public bool IsPublic { get; set; }

    public ICollection<Education> Educations { get; set; } = new List<Education>();
    public ICollection<Experience> Experiences { get; set; } = new List<Experience>();
    public ICollection<Project> Projects { get; set; } = new List<Project>();
    public ICollection<Skill> Skills { get; set; } = new List<Skill>();
    public ICollection<Certification> Certifications { get; set; } = new List<Certification>();
    public ICollection<SocialLink> SocialLinks { get; set; } = new List<SocialLink>();
}

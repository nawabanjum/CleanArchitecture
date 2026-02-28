namespace Portfolio.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IPortfolioRepository Portfolios { get; }
    IGenericRepository<Domain.Entities.Education> Educations { get; }
    IGenericRepository<Domain.Entities.Experience> Experiences { get; }
    IGenericRepository<Domain.Entities.Project> Projects { get; }
    IGenericRepository<Domain.Entities.Skill> Skills { get; }
    IGenericRepository<Domain.Entities.Certification> Certifications { get; }
    IGenericRepository<Domain.Entities.SocialLink> SocialLinks { get; }
    Task<int> SaveChangesAsync();
}

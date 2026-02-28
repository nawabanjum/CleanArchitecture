using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;
using Portfolio.Infrastructure.Data;

namespace Portfolio.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    private IPortfolioRepository? _portfolios;
    private IGenericRepository<Education>? _educations;
    private IGenericRepository<Experience>? _experiences;
    private IGenericRepository<Project>? _projects;
    private IGenericRepository<Skill>? _skills;
    private IGenericRepository<Certification>? _certifications;
    private IGenericRepository<SocialLink>? _socialLinks;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IPortfolioRepository Portfolios =>
        _portfolios ??= new PortfolioRepository(_context);

    public IGenericRepository<Education> Educations =>
        _educations ??= new GenericRepository<Education>(_context);

    public IGenericRepository<Experience> Experiences =>
        _experiences ??= new GenericRepository<Experience>(_context);

    public IGenericRepository<Project> Projects =>
        _projects ??= new GenericRepository<Project>(_context);

    public IGenericRepository<Skill> Skills =>
        _skills ??= new GenericRepository<Skill>(_context);

    public IGenericRepository<Certification> Certifications =>
        _certifications ??= new GenericRepository<Certification>(_context);

    public IGenericRepository<SocialLink> SocialLinks =>
        _socialLinks ??= new GenericRepository<SocialLink>(_context);

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}

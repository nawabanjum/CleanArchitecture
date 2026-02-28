using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Interfaces;
using Portfolio.Infrastructure.Data;

namespace Portfolio.Infrastructure.Repositories;

public class PortfolioRepository : GenericRepository<Domain.Entities.Portfolio>, IPortfolioRepository
{
    public PortfolioRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Domain.Entities.Portfolio?> GetBySlugAsync(string slug)
    {
        return await _dbSet
            .Include(p => p.Educations)
            .Include(p => p.Experiences)
            .Include(p => p.Projects)
            .Include(p => p.Skills)
            .Include(p => p.Certifications)
            .Include(p => p.SocialLinks)
            .FirstOrDefaultAsync(p => p.Slug == slug);
    }

    public async Task<Domain.Entities.Portfolio?> GetByIdWithDetailsAsync(Guid id)
    {
        return await _dbSet
            .Include(p => p.Educations)
            .Include(p => p.Experiences)
            .Include(p => p.Projects)
            .Include(p => p.Skills)
            .Include(p => p.Certifications)
            .Include(p => p.SocialLinks)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IReadOnlyList<Domain.Entities.Portfolio>> GetByUserIdAsync(string userId)
    {
        return await _dbSet
            .Where(p => p.UserId == userId)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    public async Task<bool> SlugExistsAsync(string slug)
    {
        return await _dbSet.AnyAsync(p => p.Slug == slug);
    }
}

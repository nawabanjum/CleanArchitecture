namespace Portfolio.Domain.Interfaces;

public interface IPortfolioRepository : IGenericRepository<Domain.Entities.Portfolio>
{
    Task<Domain.Entities.Portfolio?> GetBySlugAsync(string slug);
    Task<Domain.Entities.Portfolio?> GetByIdWithDetailsAsync(Guid id);
    Task<IReadOnlyList<Domain.Entities.Portfolio>> GetByUserIdAsync(string userId);
    Task<bool> SlugExistsAsync(string slug);
}

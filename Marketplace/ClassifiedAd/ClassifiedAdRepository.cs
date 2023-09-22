using Marketplace.Domain;
using Marketplace.Infrastructure;

namespace Marketplace.ClassifiedAd;

public class ClassifiedAdRepository : IClassifiedAdRepository
{
    private readonly ClassifiedAdDbContext _dbContext;

    public ClassifiedAdRepository(ClassifiedAdDbContext dbContext)
        => _dbContext = dbContext;

    public async Task Add(Domain.ClassifiedAd entity) => await _dbContext.ClassifiedAds.AddAsync(entity);

    public async Task<bool> Exists(ClassifiedAdId id) => await _dbContext.ClassifiedAds.FindAsync(id.Value) != null;

    public async Task<Domain.ClassifiedAd> Load(ClassifiedAdId id) => await _dbContext.ClassifiedAds.FindAsync(id.Value);

    public void Dispose() => _dbContext.Dispose();
}
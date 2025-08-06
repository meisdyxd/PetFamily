using Microsoft.EntityFrameworkCore;
using PetFamily.Application.SpeciesModule;
using PetFamily.Domain.SpeciesManagement;
using PetFamily.Infrastructure.Persistence.Contexts;

namespace PetFamily.Infrastructure.Persistence.SpeciesModule;

public class SpeciesRepository: ISpeciesRepository
{
    private readonly WriteDbContext _dbContext;

    public SpeciesRepository(WriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Species?> GetById(
        Guid id, 
        CancellationToken cancellationToken)
    {
        return await _dbContext.Species.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<bool> Exist(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _dbContext.Species.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        if (result == null)
            return false;
        return true;
    }

    public async Task<bool> ExistBreed(
        Guid speciesId,
        Guid breedId,
        CancellationToken cancellationToken)
    {
        var result = await _dbContext.Species.FirstOrDefaultAsync(s => s.Id == speciesId, cancellationToken);
        if (result == null)
            return false;
        return result.HasBreed(breedId);
    }
}

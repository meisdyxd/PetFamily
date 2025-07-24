using PetFamily.Domain.SpeciesManagement;

namespace PetFamily.Application.SpeciesModule;

public interface ISpeciesRepository
{
    Task<Species?> GetById(
        Guid id, 
        CancellationToken cancellationToken);
    Task<bool> Exist(
        Guid id,
        CancellationToken cancellationToken);
    Task<bool> ExistBreed(
        Guid speciesId,
        Guid breedId,
        CancellationToken cancellationToken);
}

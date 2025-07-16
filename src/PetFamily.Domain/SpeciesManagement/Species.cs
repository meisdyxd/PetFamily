using CSharpFunctionalExtensions;

namespace PetFamily.Domain.BreedManagement;

public class Species: Entity<Guid>
{
    private readonly List<Breed> _breeds = [];

    protected Species(Guid id) : base(id) { }

    public Species(string name)
    {
        Name = name;
    }

    public string Name { get; private set; }
    public IReadOnlyList<Breed> Breeds => _breeds;
}

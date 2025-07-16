using CSharpFunctionalExtensions;

namespace PetFamily.Domain.SpeciesManagement;

public class Species: Entity<Guid>
{
    private readonly List<Breed> _breeds = [];

    protected Species() : base(Guid.NewGuid()) { }

    private Species(string name)
    {
        Name = name;
    }

    public string Name { get; private set; }
    public IReadOnlyList<Breed> Breeds => _breeds;

    public Species Create(string name)
    {
        return new(name);
    }
}

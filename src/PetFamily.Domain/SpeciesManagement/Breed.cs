using CSharpFunctionalExtensions;

namespace PetFamily.Domain.SpeciesManagement;

public class Breed: Entity<Guid>
{
    private Breed() : base(Guid.NewGuid()) { }

    private Breed(string name)
    {
        Name = name;
    }

    public string Name { get; private set; }

    public static Breed Create(string name)
    {
        return new(name);
    }
}

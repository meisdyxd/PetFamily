using CSharpFunctionalExtensions;

namespace PetFamily.Domain.SpeciesManagement;

public class Breed: Entity<Guid>
{
    protected Breed(Guid id): base(id) { }

    public Breed(string name)
    {
        Name = name;
    }

    public string Name { get; private set; }
}

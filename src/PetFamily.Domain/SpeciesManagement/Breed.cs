using CSharpFunctionalExtensions;

namespace PetFamily.Domain.BreedManagement;

public class Breed: Entity<Guid>
{
    protected Breed(Guid id): base(id) { }

    private Breed(string name)
    {
        Name = name;
    }

    public string Name { get; private set; }
}

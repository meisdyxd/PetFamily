namespace PetFamily.Domain.VolunteerManagement.ValueObjects;

public class Breed
{
    public Breed(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}

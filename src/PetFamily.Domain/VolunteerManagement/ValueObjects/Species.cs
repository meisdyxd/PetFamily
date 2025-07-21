namespace PetFamily.Domain.VolunteerManagement.ValueObjects;

public class Species
{
    public Species(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}

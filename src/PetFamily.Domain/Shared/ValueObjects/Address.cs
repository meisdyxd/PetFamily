namespace PetFamily.Domain.Shared.ValueObjects;

public record Address
{
    public string Country { get; }

    public string Region { get; }

    public string City { get; }

    public string Street { get; }

    public string Flat { get; }
}

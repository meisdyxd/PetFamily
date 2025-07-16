namespace PetFamily.Domain.Shared.ValueObjects;

public record Address
{
    public string Country { get; } = null!;

    public string Region { get; } = null!;

    public string City { get; } = null!;

    public string Street { get; } = null!;

    public string Flat { get; } = null!;
}

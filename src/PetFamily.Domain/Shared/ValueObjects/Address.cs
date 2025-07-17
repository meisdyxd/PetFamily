using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Shared.ValueObjects;

public record Address
{
    //ef core
    private Address()
    {

    }

    private Address(
        string country,
        string region,
        string city,
        string street,
        string flat)
    {
        Country = country;
        Region = region;
        City = city;
        Street = street;
        Flat = flat;
    }


    public string Country { get; }

    public string Region { get; }

    public string City { get; }

    public string Street { get; }

    public string Flat { get; }

    public Result<Address, Error> Create(
        string country, 
        string region, 
        string city, 
        string street, 
        string flat)
    {
        if (string.IsNullOrWhiteSpace(country))
            return Errors.General.ValueIsInvalid(nameof(country));

        if (string.IsNullOrWhiteSpace(region))
            return Errors.General.ValueIsInvalid(nameof(region));

        if (string.IsNullOrWhiteSpace(city))
            return Errors.General.ValueIsInvalid(nameof(city));

        if (string.IsNullOrWhiteSpace(street))
            return Errors.General.ValueIsInvalid(nameof(street));

        if (string.IsNullOrWhiteSpace(flat))
            return Errors.General.ValueIsInvalid(nameof(flat));

        return new Address(country, region, city, street, flat);
    }
}

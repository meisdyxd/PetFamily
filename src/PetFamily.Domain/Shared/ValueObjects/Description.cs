using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Shared.ValueObjects;

public record Description
{
    //ef core
    private Description() { }

    private Description(string? description)
    {
        Value = description;
    }

    public string? Value { get; }

    public static Result<Description, Error> Create(string? description)
    {
        if (description == null)
            return new Description(description);

        if (string.IsNullOrWhiteSpace(description) ||
            description.Length > Constants.Description.MAX_DESCRIPTION_LENGTH)
            return Errors.General.ValueIsInvalid(nameof(Description));

        return new Description(description);
    }
}
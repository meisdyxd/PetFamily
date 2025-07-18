using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace PetFamily.Domain.Shared.ValueObjects;

public record TelephoneNumber 
{
    private TelephoneNumber() { }

    private TelephoneNumber(string number)
    {
        Value = number;
    }

    public string Value { get; } = null!;

    public static Result<TelephoneNumber, ErrorResult> Create(string number)
    {
        var telephoneRegex = new Regex(Constants.TelephoneNumber.REGEX);
        if (!telephoneRegex.IsMatch(number))
            return Errors.General.ValueIsInvalid(nameof(number));

        return new TelephoneNumber(number);
    }
}
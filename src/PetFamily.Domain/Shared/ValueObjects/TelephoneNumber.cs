using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace PetFamily.Domain.Shared.ValueObjects;

public record TelephoneNumber 
{
    private Regex TelephoneRegex = new Regex("^\\+?[7-8][0-9]{10}$");

    private TelephoneNumber() { }

    private TelephoneNumber(string number)
    {
        Value = number;
    }

    public string Value { get; } = null!;

    public Result<TelephoneNumber, Error> Create(string number)
    {
        if (!TelephoneRegex.IsMatch(number))
            return Errors.General.ValueIsInvalid(nameof(number));

        return new TelephoneNumber(number);
    }
}
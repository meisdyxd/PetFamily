using System.Text.RegularExpressions;

namespace PetFamily.Domain.Shared.ValueObjects;

public record TelephoneNumber 
{
    private Regex TelephoneRegex = new Regex("^\\+?[7-8][0-9]{10}$");
    public TelephoneNumber(string number)
    {
        if (!TelephoneRegex.IsMatch(number))
            throw new ArgumentException("Невалидный номер телефона");
        Value = number;
    }
    public string Value { get;  }
}
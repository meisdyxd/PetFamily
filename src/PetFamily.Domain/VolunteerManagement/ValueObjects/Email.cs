using System.Net.Mail;

namespace PetFamily.Domain.VolunteerManagement.ValueObjects;

public record Email
{
    public Email(string value)
    {
        if(!MailAddress.TryCreate(value, out var _))
        {
            throw new ArgumentException("Невалидная почта");
        }
        Value = value;
    }
    public string Value { get; }
}
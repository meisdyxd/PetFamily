using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using System.Net.Mail;

namespace PetFamily.Domain.VolunteerManagement.ValueObjects;

public record Email
{
    private Email(string value)
    {
        Value = value;
    }
    public string Value { get; }

    public static Result<Email, Error> Create(string email)
    {
        if (!MailAddress.TryCreate(email, out var _))
            return Errors.General.ValueIsInvalid(nameof(email));

        return new Email(email);
    }
}
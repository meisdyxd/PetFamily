using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared.Error;

namespace PetFamily.Domain.Shared.ValueObjects;

public record FullName
{
    //ef core
    private FullName() { }

    private FullName(
        string surname,
        string name,
        string? patronymic) 
    {
        Surname = surname;
        Name = name;
        Patronymic = patronymic;
    }

    public string Surname { get; }
    public string Name { get; }
    public string? Patronymic { get; }

    public static Result<FullName, ErrorResult> Create(
        string surname,
        string name,
        string? patronymic)
    {
        if (string.IsNullOrWhiteSpace(surname) ||
            surname.Length > Constants.FullName.MAX_SURNAME_LENGTH)
            return Errors.General.ValueIsInvalid(nameof(surname));

        if (string.IsNullOrWhiteSpace(name) ||
            surname.Length > Constants.FullName.MAX_NAME_LENGTH)
            return Errors.General.ValueIsInvalid(nameof(surname));

        if (patronymic is not null && (
            string.IsNullOrWhiteSpace(patronymic) ||
            surname.Length > Constants.FullName.MAX_PATRONYMIC_LENGTH))
            return Errors.General.ValueIsInvalid(nameof(surname));

        return new FullName(
            surname.Trim(),
            name.Trim(),
            patronymic?.Trim());
    }
}

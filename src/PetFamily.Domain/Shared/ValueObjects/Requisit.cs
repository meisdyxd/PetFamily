using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.VolunteerManagement.ValueObjects;

public record Requisit
{
    //ef core
    private Requisit() { }

    private Requisit(
        string name,
        string description,
        string? detailInstruction)
    {
        Name = name;
        Description = description;
        DetailInstruction = detailInstruction;
    }

    public string Name { get; }
    public string Description { get; }
    public string? DetailInstruction { get; }

    public static Result<Requisit, ErrorResult> Create(
        string name,
        string description,
        string? detailInstruction)
    {
        if (string.IsNullOrWhiteSpace(name) ||
            name.Length > Constants.Requisit.MAX_NAME_LENGTH)
            return Errors.General.ValueIsInvalid(nameof(name));

        if (string.IsNullOrWhiteSpace(description) ||
            description.Length > Constants.Requisit.MAX_DESCRIPTION_LENGTH)
            return Errors.General.ValueIsInvalid(nameof(description));

        if (detailInstruction is not null && 
            (string.IsNullOrWhiteSpace(description) ||
            description.Length > Constants.Requisit.MAX_DETAIL_INSTRUCTION_LENGTH))
            return Errors.General.ValueIsInvalid(nameof(description));

        return new Requisit(name, description, detailInstruction);
    }
}
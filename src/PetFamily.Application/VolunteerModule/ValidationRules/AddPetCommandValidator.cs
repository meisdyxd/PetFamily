using FluentValidation;
using PetFamily.Application.Validations;
using PetFamily.Application.VolunteerModule.UseCases.AddPetToVolunteer;
using PetFamily.Domain.Shared.ValueObjects;

namespace PetFamily.Application.VolunteerModule.ValidationRules;

public class AddPetCommandValidator: AbstractValidator<AddPetCommand>
{
    public AddPetCommandValidator()
    {
        RuleFor(p => p.Moniker)
            .NotEmpty()
            .WithError("moniker");

        RuleFor(p => p.Species.Id)
            .NotEmpty()
            .WithError("speciesId");

        RuleFor(p => p.Description)
            .MustBeValueObject(d => Description.Create(d.Value));

        RuleFor(p => p.Breed)
            .NotEmpty()
            .WithError("breedId");

        RuleFor(p => p.Coloration)
            .NotEmpty()
            .WithError("coloration");

        RuleFor(p => p.HealthInfo)
            .NotEmpty()
            .WithError("healthInfo");

        RuleFor(p => p.Address)
            .MustBeValueObject(a => Address.Create(a.Country, a.Region, a.City, a.Street, a.Flat));

        RuleFor(p => p.Weight)
            .Must(w => w > 0)
            .WithError("weight");

        RuleFor(p => p.Height)
            .Must(h => h > 0)
            .WithError("height");

        RuleFor(p => p.OwnerTelephoneNumber)
            .MustBeValueObject(n => TelephoneNumber.Create(n.Value));

        RuleFor(p => p.BirthDate)
            .NotEmpty()
            .WithError("birthdate");
    }
}

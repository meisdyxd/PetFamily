using FluentValidation;
using PetFamily.Application.Validations;
using PetFamily.Application.VolunteerModule.UseCases;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.VolunteerManagement.ValueObjects;

namespace PetFamily.Application.VolunteerModule.ValidationRules;

public class CreateVolunteerCommandValidator: AbstractValidator<CreateVolunteerCommand>
{
    public CreateVolunteerCommandValidator()
    {
        RuleFor(v => new { v.Surname, v.Name, v.Patronymic })
            .MustBeValueObject(f => FullName.Create(f.Surname, f.Name, f.Patronymic));

        RuleFor(v => v.Email)
            .MustBeValueObject(Email.Create);

        RuleFor(v => v.Description)
            .MustBeValueObject(Description.Create);

        RuleFor(v => v.EmployeeExperience)
            .MustBeValueObject(EmployeeExperience.Create);

        RuleFor(v => v.TelephoneNumber)
            .MustBeValueObject(TelephoneNumber.Create);

        RuleForEach(v => v.SocialNetworks)
            .MustBeValueObject(sn => SocialNetwork.Create(sn.Name, sn.Link));

        RuleForEach(v => v.Requisits)
            .MustBeValueObject(r => Requisit.Create(r.Name, r.Description, r.DetailInstruction));
    }
}

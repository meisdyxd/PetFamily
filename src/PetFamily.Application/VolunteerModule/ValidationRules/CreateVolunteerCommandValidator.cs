﻿using FluentValidation;
using PetFamily.Application.Validations;
using PetFamily.Application.VolunteerModule.UseCases.CreateVolunteer;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.VolunteerManagement.ValueObjects;

namespace PetFamily.Application.VolunteerModule.ValidationRules;

public class CreateVolunteerCommandValidator: AbstractValidator<CreateVolunteerCommand>
{
    public CreateVolunteerCommandValidator()
    {
        RuleFor(v => v.Fullname)
            .MustBeValueObject(f => FullName.Create(f.Surname, f.Name, f.Patronymic));

        RuleFor(v => v.Email)
            .MustBeValueObject(e => Email.Create(e.Value));

        RuleFor(v => v.Description)
            .MustBeValueObject(d => Description.Create(d.Value));

        RuleFor(v => v.EmployeeExperience)
            .MustBeValueObject(EmployeeExperience.Create);

        RuleFor(v => v.TelephoneNumber)
            .MustBeValueObject(tn => TelephoneNumber.Create(tn.Value));

        RuleForEach(v => v.SocialNetworks)
            .MustBeValueObject(sn => SocialNetwork.Create(sn.Name, sn.Link));

        RuleForEach(v => v.Requisits)
            .MustBeValueObject(r => Requisit.Create(r.Name, r.Description, r.DetailInstruction));
    }
}

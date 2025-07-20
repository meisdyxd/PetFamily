using FluentValidation;
using PetFamily.Application.Validations;
using PetFamily.Application.VolunteerModule.UseCases.UpdateRequisitsVolunteer;
using PetFamily.Application.VolunteerModule.UseCases.UpdateSocialNetworksVolunteer;
using PetFamily.Domain.VolunteerManagement.ValueObjects;

namespace PetFamily.Application.VolunteerModule.ValidationRules;

public class UpdateRequisitsCommandValidator : AbstractValidator<UpdateRequisitsCommand>
{
    public UpdateRequisitsCommandValidator()
    {
        RuleForEach(v => v.Requisits)
            .MustBeValueObject(r => Requisit.Create(r.Name, r.Description, r.DetailInstruction));
    }
}

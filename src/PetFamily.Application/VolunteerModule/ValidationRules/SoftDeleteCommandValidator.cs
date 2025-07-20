using FluentValidation;
using PetFamily.Application.Validations;
using PetFamily.Application.VolunteerModule.UseCases.SoftDeleteVolunteer;

namespace PetFamily.Application.VolunteerModule.ValidationRules;

public class SoftDeleteCommandValidator: AbstractValidator<SoftDeleteCommand>
{
    public SoftDeleteCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty().WithError("Id");
    }
}

using FluentValidation;
using PetFamily.Application.Validations;
using PetFamily.Application.VolunteerModule.UseCases.ForceDeleteVolunteer;

namespace PetFamily.Application.VolunteerModule.ValidationRules;

public class ForceDeleteCommandValidator: AbstractValidator<ForceDeleteCommand>
{
    public ForceDeleteCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty().WithError("Id");
    }
}

using FluentValidation;
using PetFamily.Application.Validations;
using PetFamily.Application.VolunteerModule.UseCases.MovePetPosition;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.VolunteerModule.ValidationRules;

public class MovePetPositionCommanValdiator: AbstractValidator<MovePetPositionCommand>
{
    public MovePetPositionCommanValdiator()
    {
        RuleFor(c => c.Position)
            .Must(n => n > 0)
            .WithError("Position must be greater than zero.")
            .NotNull()
            .WithError("Position and Direction cannot be null in one time.")
            .When(x => x.Direction is null);
        
        RuleFor(c => c.Direction)
            .Must(d => d == Constants.Pet.DIRECTION_BEGIN || d == Constants.Pet.DIRECTION_END)
            .WithError($"Direction must be '{Constants.Pet.DIRECTION_BEGIN}' or '{Constants.Pet.DIRECTION_END}'.")
            .NotNull()
            .WithError("Direction and Position cannot be null in one time.")
            .When(x => x.Position is null);

        RuleFor(c => new { c.Position, c.Direction })
            .Must(d =>
                (d.Position is null && d.Direction is not null)
                || (d.Position is not null && d.Direction is null))
            .WithError("Position and Direction cannot be not null in one time.");
        
        RuleFor(c => c.PetId)
            .NotEmpty()
            .WithError("PetId cannot be empty.");
        
        RuleFor(c => c.VolunteerId)
            .NotEmpty()
            .WithError("VolunteerId cannot be empty.");
    }
}
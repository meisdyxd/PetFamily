using FluentValidation;
using PetFamily.Application.VolunteerModule.UseCases.AddPhotosToPet;

namespace PetFamily.Application.VolunteerModule.ValidationRules;

public class AddPhotosToPetValidator: AbstractValidator<AddPhotosToPetCommand>
{
    public AddPhotosToPetValidator()
    {
        
    }
}
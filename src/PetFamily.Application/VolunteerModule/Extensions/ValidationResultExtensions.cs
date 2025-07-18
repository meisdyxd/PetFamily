using FluentValidation.Results;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.VolunteerModule.Extensions;

public static class ValidationResultExtensions
{
    public static Error ToError(this ValidationResult result)
    {
        var errors = new List<ErrorResponse>();

        foreach (var error in result.Errors)
        {
            errors.Add(new ErrorResponse(error.PropertyName, error.ErrorMessage));
        }

        return Error.Validation(errors);
    }
}

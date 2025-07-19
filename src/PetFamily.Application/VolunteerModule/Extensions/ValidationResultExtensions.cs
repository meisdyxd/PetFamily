using FluentValidation.Results;
using PetFamily.Domain.Shared.Error;

namespace PetFamily.Application.VolunteerModule.Extensions;

public static class ValidationResultExtensions
{
    public static ErrorResult ToError(this ValidationResult result)
    {
        var errors = new List<ErrorResponse>();

        foreach (var error in result.Errors)
        {
            errors.Add(ErrorResponse.Validation(error.PropertyName, error.ErrorMessage));
        }

        return ErrorResult.Create(errors);
    }
}

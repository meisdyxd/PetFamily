using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Validations;

public static class CustomValidators
{
    public static IRuleBuilderOptionsConditions<T, TElement> MustBeValueObject<T, TElement, TValueOobject>(
        this IRuleBuilder<T, TElement> ruleBuilder,
        Func<TElement, Result<TValueOobject, Error>> method)
    {
        return ruleBuilder.Custom((value, context) =>
        {
            Result<TValueOobject, Error> result = method(value);

            if (result.IsSuccess)
                return;

            foreach(var error in result.Error.Errors)
            {
                context.AddFailure(error.Code, error.Message);
            }
        });
    }
}

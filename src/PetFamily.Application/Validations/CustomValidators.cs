using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.Domain.Shared.Error;

namespace PetFamily.Application.Validations;

public static class CustomValidators
{
    public static IRuleBuilderOptionsConditions<T, TElement> MustBeValueObject<T, TElement, TValueOobject>(
        this IRuleBuilder<T, TElement> ruleBuilder,
        Func<TElement, Result<TValueOobject, ErrorResult>> method)
    {
        return ruleBuilder.Custom((value, context) =>
        {
            Result<TValueOobject, ErrorResult> result = method(value);

            if (result.IsSuccess)
                return;

            foreach(var error in result.Error.Errors)
            {
                context.AddFailure(error.Code, error.Message);
            }
        });
    }

    public static IRuleBuilderOptions<T, TElement> WithError<T, TElement>(
        this IRuleBuilderOptions<T, TElement> rule,
        string? propertyName)
    {
        var error = Errors.General.ValueIsInvalid(propertyName).Errors.First();

        return rule.WithMessage(error.Message).OverridePropertyName(error.Code);
    }
}

using PetFamily.Domain.Shared.Error;

namespace PetFamily.Domain.Shared.Extensions;

public static class ErrorExtension
{
    public static ErrorResponse GetError(this ErrorResult errorResult)
    {
        if (errorResult.Errors.Count == 0)
        {
            throw new ArgumentException("Error result dont have errors");
        }

        return errorResult.Errors.First();
    }
}

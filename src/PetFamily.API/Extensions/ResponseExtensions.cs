using Microsoft.AspNetCore.Mvc;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Error;

namespace PetFamily.API.Extensions;

public static class ResponseExtensions
{
    public static ActionResult ToResponse(this ErrorResult errorResult)
    {
        var errorType = errorResult.Errors.First().ErrorType;
        var statusCode = Enum.Parse<ErrorTypes>(errorType) switch
        {
            ErrorTypes.Validation => StatusCodes.Status400BadRequest,
            ErrorTypes.BadRequest => StatusCodes.Status400BadRequest,
            ErrorTypes.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorTypes.Conflict => StatusCodes.Status409Conflict,
            ErrorTypes.NotFound => StatusCodes.Status404NotFound,
            ErrorTypes.Forbidden => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError,
        };

        return new ObjectResult(Envelope.Failure(errorResult.Errors))
        {
            StatusCode = statusCode,
        };
    }
}

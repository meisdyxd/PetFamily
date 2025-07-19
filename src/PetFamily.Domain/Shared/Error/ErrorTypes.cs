namespace PetFamily.Domain.Shared.Error;

public enum ErrorTypes
{
    Validation,
    BadRequest,
    Unauthorized,
    Conflict,
    NotFound,
    Forbidden,
    InternalServerError
}

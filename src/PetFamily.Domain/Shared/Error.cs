namespace PetFamily.Domain.Shared;

public record Error
{
    public List<ErrorResponse> Errors { get; } 
    public ErrorType ErrorType { get; }

    private Error(
        List<ErrorResponse> errors,
        ErrorType errorType)
    {
        Errors = errors;
        ErrorType = errorType;
    }

    public static Error Validation(List<ErrorResponse> errors) =>
        new(errors, ErrorType.Validation);

    public static Error BadRequest(List<ErrorResponse> errors) =>
        new(errors, ErrorType.BadRequest);

    public static Error Unauthorized(List<ErrorResponse> errors) =>
        new(errors, ErrorType.Unauthorized);

    public static Error Conflict(List<ErrorResponse> errors) =>
        new(errors, ErrorType.Conflict);

    public static Error NotFound(List<ErrorResponse> errors) =>
        new(errors, ErrorType.NotFound);

    public static Error Forbidden(List<ErrorResponse> errors) =>
        new(errors, ErrorType.Forbidden);

    public static Error Create(
        List<ErrorResponse> errors,
        ErrorType errorType)
    {
        return new(
            errors,
            errorType);
    }
}
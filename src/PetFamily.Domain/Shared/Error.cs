namespace PetFamily.Domain.Shared;

public record Error
{
    public string Code { get; }
    public string Message { get; }
    public ErrorType ErrorType { get; }

    private Error(
        string code, 
        string message, 
        ErrorType errorType)
    {
        Code = code;
        Message = message;
        ErrorType = errorType;
    }

    public static Error Validation(string code, string message) =>
        new(code, message, ErrorType.Validation);

    public static Error BadRequest(string code, string message) =>
        new(code, message, ErrorType.BadRequest);

    public static Error Unauthorized(string code, string message) =>
        new(code, message, ErrorType.Unauthorized);

    public static Error Conflict(string code, string message) =>
        new(code, message, ErrorType.Conflict);

    public static Error NotFound(string code, string message) =>
        new(code, message, ErrorType.NotFound);

    public static Error Forbidden(string code, string message) =>
        new(code, message, ErrorType.Forbidden);

    public static Error Create(
        string code, 
        string message,
        ErrorType errorType)
    {
        return new(
            code,
            message,
            errorType);
    }
}

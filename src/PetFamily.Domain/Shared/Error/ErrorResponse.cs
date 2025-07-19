namespace PetFamily.Domain.Shared.Error;

public record ErrorResponse
{
    public ErrorResponse(
        string code,
        string message,
        string errorType)
    {
        Code = code;
        Message = message;
        ErrorType = errorType;
    }

    public string Code { get; }
    public string Message { get; }
    public string ErrorType { get; }

    public static ErrorResponse Validation(string code, string message)
        => new ErrorResponse(code, message, ErrorTypes.Validation.ToString());

    public static ErrorResponse NotFound(string code, string message)
        => new ErrorResponse(code, message, ErrorTypes.NotFound.ToString());

    public static ErrorResponse Internal(string code, string message)
        => new ErrorResponse(code, message, ErrorTypes.InternalServerError.ToString());
}
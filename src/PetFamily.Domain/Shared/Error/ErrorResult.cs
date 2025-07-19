namespace PetFamily.Domain.Shared.Error;

public record ErrorResult
{
    public List<ErrorResponse> Errors { get; } 
    

    private ErrorResult(List<ErrorResponse> errors)
    {
        Errors = errors;
    }

    public static ErrorResult Create(
        List<ErrorResponse> errors)
    {
        return new ErrorResult(errors);
    }
}
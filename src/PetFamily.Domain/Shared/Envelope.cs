using PetFamily.Domain.Shared.Error;

namespace PetFamily.Domain.Shared;

public record Envelope
{
    private Envelope(
        object? result,
        List<ErrorResponse>? errors, 
        DateTime generateTime)
    {
        Result = result;
        Errors = errors;
        GenerateTime = generateTime;
    }

    public object? Result {  get; }
    public List<ErrorResponse>? Errors { get; }
    public DateTime GenerateTime { get; }

    public static Envelope Successful(object? result)
        => new(result, null, DateTime.UtcNow);

    public static Envelope Failure(List<ErrorResponse>? errors)
        => new(null, errors, DateTime.UtcNow);
}

namespace PetFamily.Domain.Shared;

public static class Errors
{
    public static class General
    {
        public static Error ValueIsInvalid(string? name)
        {
            var label = name ?? "value";
            var error = new List<ErrorResponse>
            {
                new ErrorResponse("value.is.invalid", $"{label} is invalid")
            };

            return Error.Validation(error);
        }

        public static Error RecordNotFound(Guid? id)
        {
            var label = id is null ? "" : $" for id: '{id}'";
            var error = new List<ErrorResponse>
            {
                new ErrorResponse("record.not.found", $"record not found{label}")
            };

            return Error.NotFound(error);
        }

        public static Error ValueIsRequired(string? name)
        {
            var label = name ?? "value";
            var error = new List<ErrorResponse>
            {
                new ErrorResponse("value.is.requred", $"{label} is required")
            };

            return Error.Validation(error);
        }
    }
}

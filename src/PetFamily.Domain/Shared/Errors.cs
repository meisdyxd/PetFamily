namespace PetFamily.Domain.Shared;

public static class Errors
{
    public static class General
    {
        public static Error ValueIsInvalid(string? name)
        {
            var label = name ?? "value";

            return Error.Validation("value.is.invalid", $"{label} is invalid");
        }

        public static Error RecordNotFound(Guid? id)
        {
            var label = id is null ? "" : $" for id: '{id}'";

            return Error.NotFound("record.not.found", $"record not found{label}");
        }

        public static Error ValueIsRequired(string? name)
        {
            var label = name ?? "value";

            return Error.Validation("value.is.requred", $"{label} is required");
        }
    }
}

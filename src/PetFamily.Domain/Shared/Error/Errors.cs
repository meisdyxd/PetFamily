namespace PetFamily.Domain.Shared.Error;

public static class Errors
{
    public static class General
    {
        public static ErrorResult ValueIsInvalid(string? name)
        {
            var label = name ?? "value";
            var error = new List<ErrorResponse>
            {
                ErrorResponse.Validation("value.is.invalid", $"{label} is invalid")
            };

            return ErrorResult.Create(error);
        }

        public static ErrorResult RecordNotFound(Guid? id)
        {
            var label = id is null ? "" : $" for id: '{id}'";
            var error = new List<ErrorResponse>
            {
                ErrorResponse.NotFound("record.not.found", $"record not found{label}")
            };

            return ErrorResult.Create(error);
        }

        public static ErrorResult ValueIsRequired(string? name)
        {
            var label = name ?? "value";
            var error = new List<ErrorResponse>
            {
                ErrorResponse.Validation("value.is.requred", $"{label} is required")
            };

            return ErrorResult.Create(error);
        }

        public static ErrorResult InternalServerError(string? place)
        {
            var error = new List<ErrorResponse>
            {
                ErrorResponse.Internal("internal.server.error", $"{place}")
            };

            return ErrorResult.Create(error);
        }
    }

    public static class Minio 
    {
        public static ErrorResult UploadError(string message, string errorCode)
        {
            var error = new List<ErrorResponse>
            {
                new ErrorResponse("upload.file", message, errorCode)
            };

            return ErrorResult.Create(error);
        }
    }
}

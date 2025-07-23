using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Error;
using PetFamily.Domain.Shared.Extensions;

namespace PetFamily.API.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate requestDelegate, ILogger<ExceptionMiddleware> logger)
    {
        _next = requestDelegate;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            var errorResult = Errors.General.InternalServerError(ex.Message);
            var message = Envelope.Failure([errorResult.GetError()]);

            await context.Response.WriteAsJsonAsync(message, typeof(Envelope));
            _logger.LogCritical("An occured error. {message}.\n{stacktrace}", ex.Message, ex.StackTrace);
        }
        
    }
}

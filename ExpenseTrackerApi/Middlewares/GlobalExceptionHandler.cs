using Microsoft.AspNetCore.Diagnostics;

namespace ExpenseTrackerApi.Middlewares;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "A critical unhandled exception occurred.");

        var safeResponse = new 
        {
            statusCode = 500,
            message = "An unexpected internal server error occurred. Our technical team has been notified."
        };

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(safeResponse, cancellationToken);

        return true;
    }
}
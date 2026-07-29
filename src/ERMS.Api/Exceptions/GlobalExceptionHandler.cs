using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ERMS.Api.Exceptions
{
    public sealed class GlobalExceptionHandler : IExceptionHandler 
    { 
        private readonly ILogger<GlobalExceptionHandler> _logger; 
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) 
        { 
            _logger = logger; 
        } 
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        { 
            _logger.LogError(exception, "Unhandled exception occurred."); 
            var problem = new ProblemDetails { 
                Status = StatusCodes.Status500InternalServerError,
                Title = "Server Error", 
                Detail = "An unexpected error occurred.",
                Instance = httpContext.Request.Path
            };
            httpContext.Response.StatusCode = problem.Status.Value; 
            await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken); return true;
        }
    }
}

using ERMS.SharedKernel.Errors;
using ERMS.SharedKernel.Results;

namespace ERMS.Api.Extensions
{
    public static class ResultExtensions 
    { 
        public static IResult ToHttpResult(this Result result) 
        { 
            if (result.IsSuccess)
            { 
                return Results.NoContent();
            }
            return CreateFailureResult(result.Error);
        } 
        public static IResult ToHttpResult<T>(this Result<T> result) 
        { 
            if (result.IsSuccess) 
            {
                return Results.Ok(result.Value); 
            } return CreateFailureResult(result.Error);
        } 
        private static IResult CreateFailureResult(Error error) 
        { 
            return error.Type switch { 
                ErrorType.Validation => Results.BadRequest(error),
                ErrorType.NotFound => Results.NotFound(error),
                ErrorType.Conflict => Results.Conflict(error),
                ErrorType.Unauthorized => Results.Unauthorized(), 
                ErrorType.Forbidden => Results.StatusCode(StatusCodes.Status403Forbidden), 
                _ => Results.Problem(
                    title: error.Code,
                    detail: error.Description,
                    statusCode: StatusCodes.Status500InternalServerError) 
            };
        } 
    }
}

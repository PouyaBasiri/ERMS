using System;
using System.Collections.Generic;
using System.Text;

namespace ERMS.SharedKernel.Errors
{
    public class CommonErrors
    {
        public static readonly Error Unexpected =
        Error.Failure(
            "Common.Unexpected",
            "An unexpected error occurred.");

        public static readonly Error Validation =
            Error.Validation(
                "Common.Validation",
                "One or more validation errors occurred.");

        public static readonly Error NotFound =
            Error.NotFound(
                "Common.NotFound",
                "The requested resource was not found.");

        public static readonly Error Unauthorized =
            Error.Unauthorized(
                "Common.Unauthorized",
                "Authentication is required.");

        public static readonly Error Forbidden =
            Error.Forbidden(
                "Common.Forbidden",
                "You do not have permission to perform this action.");

        public static readonly Error Conflict =
            Error.Conflict(
                "Common.Conflict",
                "The requested operation conflicts with the current resource state.");
    }
}

using ERMS.SharedKernel.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERMS.Domain.Users
{
    public static class UserErrors
    {
        public static readonly Error EmailIsRequired =
            new(
                "User.EmailRequired",
                "Email is required.",
                ErrorType.Validation);

        public static readonly Error InvalidEmail =
            new(
                "User.InvalidEmail",
                "Email format is invalid.",
                ErrorType.Validation);

        public static readonly Error AlreadyActive =
            new(
                "User.AlreadyActive",
                "User is already active.",
                ErrorType.Conflict);

        public static readonly Error AlreadyInactive =
            new(
                "User.AlreadyInactive",
                "User is already inactive.",
                ErrorType.Conflict);
    }
}

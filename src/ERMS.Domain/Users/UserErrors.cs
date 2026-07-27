using ERMS.SharedKernel.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERMS.Domain.Users
{
    public static class UserErrors
    {
        public static readonly Error FirstNameRequired =
         Error.Validation(
             "User.FirstNameRequired",
             "First name is required.");

        public static readonly Error LastNameRequired =
            Error.Validation(
                "User.LastNameRequired",
                "Last name is required.");

        public static readonly Error EmailRequired =
            Error.Validation(
                "User.EmailRequired",
                "Email is required.");

        public static readonly Error InvalidEmail =
            Error.Validation(
                "User.InvalidEmail",
                "Email format is invalid.");

        public static readonly Error EmailTooLong =
            Error.Validation(
                "User.EmailTooLong",
                $"Email cannot exceed {UserConstants.EmailMaxLength} characters.");

        public static readonly Error FirstNameTooLong =
            Error.Validation(
                "User.FirstNameTooLong",
                $"First name cannot exceed {UserConstants.FirstNameMaxLength} characters.");

        public static readonly Error LastNameTooLong =
            Error.Validation(
                "User.LastNameTooLong",
                $"Last name cannot exceed {UserConstants.LastNameMaxLength} characters.");
    }
}

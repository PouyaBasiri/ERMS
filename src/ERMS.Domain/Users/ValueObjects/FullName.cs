using ERMS.Domain.Users;
using ERMS.SharedKernel;
using ERMS.SharedKernel.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERMS.Domain.Users.ValueObjects
{
    public sealed class FullName : ValueObject
    {
        private FullName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; }

        public string LastName { get; }

        public string DisplayName =>
            $"{FirstName} {LastName}";

        public static Result<FullName> Create(string? firstName,string? lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                return Result<FullName>.Failure(
                    UserErrors.FirstNameRequired);
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                return Result<FullName>.Failure(
                    UserErrors.LastNameRequired);
            }

            firstName = firstName.Trim();
            lastName = lastName.Trim();

            if (firstName.Length > UserConstants.FirstNameMaxLength)
            {
                return Result<FullName>.Failure(
                    UserErrors.FirstNameTooLong);
            }

            if (lastName.Length > UserConstants.LastNameMaxLength)
            {
                return Result<FullName>.Failure(
                    UserErrors.LastNameTooLong);
            }

            return Result<FullName>.Success(
                new FullName(firstName, lastName));
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return FirstName.ToUpperInvariant();
            yield return LastName.ToUpperInvariant();
        }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
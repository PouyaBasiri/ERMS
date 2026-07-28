using ERMS.SharedKernel;
using ERMS.SharedKernel.Results;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace ERMS.Domain.Users.ValueObjects
{
    public sealed class Email : ValueObject
    {
        private Email(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static Result<Email> Create(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return Result<Email>.Failure(UserErrors.EmailRequired);
            }

            value = value.Trim();

            if (value.Length > UserConstants.EmailMaxLength)
            {
                return Result<Email>.Failure(UserErrors.EmailTooLong);
            }

            if (!IsValid(value))
            {
                return Result<Email>.Failure(UserErrors.InvalidEmail);
            }

            return Result<Email>.Success(new Email(value));
        }

        private static bool IsValid(string email)
        {
            try
            {
                var address = new MailAddress(email);

                return address.Address.Equals(
                    email,
                    StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value.ToUpperInvariant();
        }

        public override string ToString()
        {
            return Value;
        }

        public static implicit operator string(Email email)
        {
            return email.Value;
        }
    }
}

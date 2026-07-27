using ERMS.SharedKernel.Abstractions;
using ERMS.SharedKernel.Results;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace ERMS.Domain.ValueObjects
{
    public sealed class Email : ValueObject
    {
        private Email(string value)
        {
            Value = value;
        }
        public string Value { get; }

        public static bool TryCreate(string? value,out Email? email)
        {
            email = null;

            if (string.IsNullOrWhiteSpace(value))
                return false;

            try
            {
                _ = new MailAddress(value);
                email = new Email(value.Trim());
                return true;
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
        public override string ToString()=> Value;
    }
}

using ERMS.SharedKernel.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERMS.Domain.ValueObjects
{
    public sealed class FullName : ValueObject
    {
        private FullName(
            string firstName,
            string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; }
        public string LastName { get; }
        public string DisplayName =>$"{FirstName} {LastName}";

        public static bool TryCreate(string? firstName,string? lastName,out FullName? fullName)
        {
            fullName = null;

            if (string.IsNullOrWhiteSpace(firstName))
                return false;

            if (string.IsNullOrWhiteSpace(lastName))
                return false;

            fullName = new FullName(firstName.Trim(),lastName.Trim());
            return true;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return FirstName.ToUpperInvariant();
            yield return LastName.ToUpperInvariant();
        }

        public override string ToString()=> DisplayName;
    }
}
using ERMS.SharedKernel.Errors;
using ERMS.SharedKernel.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERMS.Domain.Users.Rules
{
    public sealed class UserEmailMustBeUniqueRule : BusinessRule
    {
        private readonly bool _emailExists; public UserEmailMustBeUniqueRule(bool emailExists) { _emailExists = emailExists; }
        public override bool IsBroken() { return _emailExists; }
        public override Error Error => Error.Conflict("User.EmailAlreadyExists", "A user with the same email already exists.");
    }
}

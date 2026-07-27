using ERMS.SharedKernel.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERMS.SharedKernel.Rules
{
    public abstract class BusinessRule : IBusinessRule
    {
        public abstract bool IsBroken();
        public abstract Error Error { get; }
    }
}

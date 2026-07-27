using ERMS.SharedKernel.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERMS.SharedKernel.Rules
{
    public interface IBusinessRule
    {
        bool IsBroken();
        Error Error { get; }
    }
}

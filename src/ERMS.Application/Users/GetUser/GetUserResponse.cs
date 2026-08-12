using System;
using System.Collections.Generic;
using System.Text;

namespace ERMS.Application.Users.GetUser
{
    public sealed record GetUserResponse (Guid Id,string FullName,string Email);
}

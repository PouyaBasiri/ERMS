using System;
using System.Collections.Generic;
using System.Text;

namespace ERMS.Domain.Users.CreateUser
{
    public sealed record CreateUserResponse(Guid Id, string FullName, string Email);
}

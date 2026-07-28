using ERMS.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERMS.Domain.Users.CreateUser
{
    public sealed record CreateUserCommand(string FirstName, string LastName, string Email) : ICommand<CreateUserResponse>;
}

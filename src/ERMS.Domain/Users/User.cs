using ERMS.Domain.ValueObjects;
using ERMS.SharedKernel.Abstractions;
using ERMS.SharedKernel.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERMS.Domain.Users
{
    public sealed class User : AggregateRoot
    {
        private User(Guid id,FullName fullName,Email email): base(id)
        {
            FullName = fullName;
            Email = email;
            IsActive = true;
        }

        public FullName FullName { get; private set; }

        public Email Email { get; private set; }

        public bool IsActive { get; private set; }

        public static Result<User> Create(
            FullName fullName,
            Email email)
        {
            var user = new User(Guid.NewGuid(),fullName,email);

            user.RaiseDomainEvent(new UserCreatedEvent(user.Id));

            return Result<User>.Success(user);
        }

        public Result ChangeEmail(
            Email email)
        {
            if (Email == email)
                return Result.Success();

            Email = email;

            return Result.Success();
        }

        public Result Activate()
        {
            if (IsActive)
                return Result.Success();

            IsActive = true;

            return Result.Success();
        }

        public Result Deactivate()
        {
            if (!IsActive)
                return Result.Success();

            IsActive = false;

            return Result.Success();
        }
    }
}

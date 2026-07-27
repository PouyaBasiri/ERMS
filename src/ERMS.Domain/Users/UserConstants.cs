using ERMS.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERMS.Domain.Users
{
    public sealed class UserCreatedEvent : DomainEvent
    {
        public UserCreatedEvent(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}

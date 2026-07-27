using System;
using System.Collections.Generic;
using System.Text;

namespace ERMS.Domain.Events
{
    public sealed class UserCreatedEvent:DomainEvent
    {
        public UserCreatedEvent(Guid userId)
        {
            UserId = userId;
        }
        public Guid UserId { get; }
    }
}

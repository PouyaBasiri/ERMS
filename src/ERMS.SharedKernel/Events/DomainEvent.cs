using ERMS.SharedKernel.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERMS.Domain.Events
{
    public abstract class DomainEvent : IDomainEvent
    {
        protected DomainEvent()
        {
            Id = Guid.NewGuid();

            OccurredOnUtc = DateTime.UtcNow;
        }


        public Guid Id { get; }


        public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;
    }
}

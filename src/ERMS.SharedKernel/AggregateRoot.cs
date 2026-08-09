using ERMS.Domain.Events;
using ERMS.SharedKernel.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERMS.SharedKernel
{
    public abstract class AggregateRoot : Entity
    {
        private readonly List<IDomainEvent> _domainEvents = [];

        protected AggregateRoot(Guid id)
            : base(id)
        {
        }
        protected AggregateRoot(): base(Guid.Empty)
        {
        }

        public IReadOnlyCollection<IDomainEvent> DomainEvents =>
            _domainEvents.AsReadOnly();

        protected void RaiseDomainEvent(
            IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}

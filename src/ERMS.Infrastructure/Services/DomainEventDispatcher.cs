using ERMS.SharedKernel;
using ERMS.SharedKernel.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERMS.Infrastructure.Services
{
    public sealed class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IPublisher _publisher;

        public DomainEventDispatcher(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public async Task DispatchAsync(
            IReadOnlyCollection<IDomainEvent> events,
            CancellationToken cancellationToken = default)
        {
            foreach (var domainEvent in events)
            {
                await _publisher.Publish(domainEvent, cancellationToken);
            }
        }
    }
}

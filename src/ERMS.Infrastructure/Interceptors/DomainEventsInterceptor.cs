using ERMS.SharedKernel;
using ERMS.SharedKernel.Events;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERMS.Infrastructure.Interceptors
{
    public sealed class DomainEventsInterceptor : SaveChangesInterceptor
    {
        private readonly IDomainEventDispatcher _dispatcher;

        public DomainEventsInterceptor(
            IDomainEventDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public override async ValueTask<int> SavedChangesAsync(
            SaveChangesCompletedEventData eventData,
            int result,
            CancellationToken cancellationToken = default)
        {
            if (eventData.Context is null)
            {
                return await base.SavedChangesAsync(
                    eventData,
                    result,
                    cancellationToken);
            }

            var aggregates = eventData.Context.ChangeTracker
                .Entries<AggregateRoot>()
                .Select(e => e.Entity)
                .Where(e => e.DomainEvents.Any())
                .ToList();

            var domainEvents = aggregates
                .SelectMany(e => e.DomainEvents)
                .ToList();

            foreach (var aggregate in aggregates)
            {
                aggregate.ClearDomainEvents();
            }

            await _dispatcher.DispatchAsync(
                domainEvents,
                cancellationToken);

            return await base.SavedChangesAsync(
                eventData,
                result,
                cancellationToken);
        }
    }
}

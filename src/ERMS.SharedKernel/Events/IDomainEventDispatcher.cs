using System;
using System.Collections.Generic;
using System.Text;

namespace ERMS.SharedKernel.Events
{
    public interface IDomainEventDispatcher
    {
        Task DispatchAsync(IReadOnlyCollection<IDomainEvent> events,CancellationToken cancellationToken = default);
    }
}

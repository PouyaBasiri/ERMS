using System;
using System.Collections.Generic;
using System.Text;

namespace ERMS.Application.Abstractions.Persistence
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default);
    }
}

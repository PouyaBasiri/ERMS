using System;
using System.Collections.Generic;
using System.Text;

namespace ERMS.Domain.Users
{
    public interface IUserReadRepository
    {
        Task<User?> GetByIdAsync(Guid id,CancellationToken cancellationToken = default);

        Task<IReadOnlyList<User>> GetAllAsync(
            CancellationToken cancellationToken = default);
    }
}

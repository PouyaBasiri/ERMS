using System;
using System.Collections.Generic;
using System.Text;

namespace ERMS.Domain.Users
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(
       Guid id,
       CancellationToken cancellationToken = default);

        Task<User?> GetByEmailAsync(
            string email,
            CancellationToken cancellationToken = default);

        Task<bool> ExistsByEmailAsync(
            string email,
            CancellationToken cancellationToken = default);

        Task AddAsync(
            User user,
            CancellationToken cancellationToken = default);

        void Update(User user);

        void Remove(User user);
    }
}

using ERMS.Domain.Users;
using ERMS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERMS.Infrastructure.Repositories
{
    public sealed class UserReadRepository :  IUserReadRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public UserReadRepository(ApplicationDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id ,cancellationToken);
        }

        public async Task<(IReadOnlyList<User> Users, int TotalCount)> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Users.AsNoTracking().OrderBy(user => user.Id);
            var totalCount = await query.CountAsync(cancellationToken);

            var users = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            return (users, totalCount);
        }
    }
}

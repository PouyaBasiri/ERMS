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

        public async Task<IReadOnlyList<User>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users.ToListAsync(cancellationToken);
        }

        public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id ,cancellationToken);
        }
    }
}

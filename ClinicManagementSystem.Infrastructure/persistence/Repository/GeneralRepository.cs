using ClinicManagementSystem.Domain.Abstractions.IRepository;
using ClinicManagementSystem.Domain.Abstractions.IRepository.ClinicManagementSystem.Domain.Abstractions.IRepository;
using ClinicManagementSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicManagementSystem.Infrastructure.Persistence.Repository
{
    public class GeneralRepository<T> : IGeneralRepository<T> where T : class
    {
        private readonly ClinicDbContext _context;
        internal DbSet<T> dbset;

        public GeneralRepository(ClinicDbContext context)
        {
            _context = context;
            dbset = context.Set<T>();
        }

        // 🟢 Add
        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await dbset.AddAsync(entity, cancellationToken);
        }

        // 🟢 Get one
        public async Task<T?> GetAsync(
            Expression<Func<T, bool>> filter,
            CancellationToken cancellationToken = default)
        {
            return await dbset
                .Where(filter)
                .FirstOrDefaultAsync(cancellationToken);
        }

        // 🟢 Get all (FIXED)
        public async Task<List<T>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            return await dbset.ToListAsync(cancellationToken);
        }

        // 🟢 Update
        public void Update(T entity)
        {
            dbset.Update(entity);
        }

        // 🟢 Delete
        public void Delete(T entity)
        {
            dbset.Remove(entity);
        }

        // 🟢 Delete range
        public void DeleteRange(IEnumerable<T> entities)
        {
            dbset.RemoveRange(entities);
        }
    }
}
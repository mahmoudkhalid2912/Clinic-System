using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicManagementSystem.Domain.Abstractions.IRepository
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    namespace ClinicManagementSystem.Domain.Abstractions.IRepository
    {
        public interface IGeneralRepository<T> where T : class
        {
            Task<T?> GetAsync(
                Expression<Func<T, bool>> filter,
                CancellationToken cancellationToken = default);

            Task<List<T>> GetAllAsync(
                CancellationToken cancellationToken = default);

            Task AddAsync(
                T entity,
                CancellationToken cancellationToken = default);

            void Update(T entity);

            void Delete(T entity);

            void DeleteRange(IEnumerable<T> entities);
        }
    }
}
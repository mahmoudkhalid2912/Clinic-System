
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using ClinicManagementSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using ClinicManagementSystem.Domain.Abstractions.IRepository;
namespace ClinicManagementSystem.Infrastructure.persistence.Repository
{
    public class GeneralRepository<T> : IGeneralRepository<T> where T : class
    {

        private readonly ClinicDbContext _context;
        internal DbSet<T> dbset;

        public GeneralRepository(ClinicDbContext context)
        {
            _context=context;
            dbset = context.Set<T>();
        }


        public void Add(T entity)
        {
            dbset.Add(entity);
        }

        public void Delete(T entity)
        {
            dbset.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> Entites)
        {
            dbset.RemoveRange(Entites);
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query=dbset;
            query = dbset.Where(filter);

            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query=dbset;
            return query.ToList();
            
        }

        public void Update(T entity)
        {
           dbset.Update(entity);

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ClinicManagementSystem.Domain.Abstractions.IRepository
{
    public interface IGeneralRepository<T> where T : class
    {
        T Get(Expression<Func<T,bool>>filter); // to get one by specific filter you choose 

        IEnumerable<T> GetAll(); // to get all

        void Add(T entity); 

        void Update(T entity); 

        void Delete(T entity);

        void DeleteRange(IEnumerable<T> Entites);

    }
}

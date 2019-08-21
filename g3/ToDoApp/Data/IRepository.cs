using System;
using System.Collections.Generic;

namespace Data
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}

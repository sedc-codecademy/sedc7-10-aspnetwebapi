using SEDC.Loto3000.Models;
using System.Collections.Generic;

namespace SEDC.Loto3000.DataLayer.Contracts
{
    public interface IGenericRepository<T> where T : BaseModel
    {
        IEnumerable<T> GetAll();

        T GetById(string id);

        void Add(T item);

        T Update(T item);

        bool Delete(T item);
    }
}

using SEDC.Loto3000.DataLayer.Contracts;
using SEDC.Loto3000.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SEDC.Loto3000.DataLayer.Implementations
{
    public class InMemoryGenericRepository<T> : IGenericRepository<T> 
        where T : BaseModel
    {
        private readonly IList<T> _items = new List<T>();

        public void Add(T item)
        {
            item.Id = Guid.NewGuid().ToString();
            _items.Add(item);
        }

        public bool Delete(T item)
        {
            return _items.Remove(item);
        }

        public IEnumerable<T> GetAll()
        {
            return _items;
        }

        public T GetById(string id)
        {
            return _items.SingleOrDefault(i => i.Id == id);
        }

        public T Update(T item)
        {
            //it is workaround but if item is read first by this repository and then updated is ok
            return item;
        }
    }
}

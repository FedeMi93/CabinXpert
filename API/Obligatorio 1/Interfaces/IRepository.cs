using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio_1.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public void Add(T item);
        public void Update(T item);
        public void Delete(T item);
        public T GetById(int id);

        public IEnumerable<T> GetAll();

    }
}

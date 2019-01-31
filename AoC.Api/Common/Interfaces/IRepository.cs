using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IRepository<T>
    {
        T GetById(int index);
        IEnumerable<T> GetAll();
        void Create(T entity);
        void Delete(T entity);
        T Update(T entity);
    }
}

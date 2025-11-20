using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.demo.core.Interface.Service
{
    public interface IBaseService<T>
    {
        IEnumerable<T> GetAll();
        
        T Get(Guid id);

        T GetById(Guid id);

        int Insert(T entity);

        int Update(T entity, Guid id);

        int Delete(Guid id);

        int DeleteMany(List<Guid> ids);

    }
}

using Misa.demo.core.Interface.Repository;
using Misa.demo.core.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.demo.core.Service
{
    public class BaseService<T>: IBaseService<T>
    {
        readonly IBaseRepo<T> _baseRepo;
        public BaseService(IBaseRepo<T> baseRepo) { 
                _baseRepo = baseRepo;
        }
        public IEnumerable<T> GetAll()
        {
            return _baseRepo.GetAll();
        }

        public T Get(Guid id)
        {
            var entity= _baseRepo.Get(id);
            return entity;
        }

        public T GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public int Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public int Update(T entity, Guid id)
        {
            throw new NotImplementedException();
        }

        public int Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        
    }
}

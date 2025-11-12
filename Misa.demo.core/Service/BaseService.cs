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
            Validate(entity, "Insert");
            return _baseRepo.Insert(entity);
        }

        public int Update(T entity, Guid id)
        {
            Validate(entity, "Update");
            return _baseRepo.Update(entity, id);
        }

        public int Delete(Guid id)
        {
            return _baseRepo.Delete(id);
        }


        /// <summary>
        /// Hàm validate chung, các Service con (như CustomerService)
        /// có thể override để thêm logic validate cụ thể.
        /// </summary>
        /// <param name="entity">Đối tượng</param>
        /// <param name="mode">"Insert" hay "Update"</param>
        protected virtual void Validate(T entity, string mode)
        {
            // Nơi để logic validate chung
            // Ví dụ: kiểm tra các trường bắt buộc chung...
            // Hoặc tốt hơn là dùng FluentValidation (xem mục 4)
        }

    }
}

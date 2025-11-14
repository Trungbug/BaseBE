using Misa.demo.core.Attibute;
using Misa.demo.core.Exceptions;
using Misa.demo.core.Interface.Repository;
using Misa.demo.core.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Misa.demo.core.Service
{
    public class BaseService<T>: IBaseService<T>
    {
        protected readonly IBaseRepo<T> _baseRepo;
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
            if(entity == null)
            {
                throw new NotFoundException($"Không tìm thấy tài nguyên có id <{id}>");
            }
            return entity;
        }

        public T GetById(Guid id)
        {
            return Get(id);
        }

        public int Insert(T entity)
        {
            Validate(entity, "Insert");
           return _baseRepo.Insert(entity);
        }

        public int Update(T entity, Guid id)
        {
            Get(id);

            Validate(entity, "Update");
            return _baseRepo.Update(entity, id);
        }

        public int Delete(Guid id)
        {
            Get(id); 

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
            var props = entity.GetType().GetProperties();

            foreach (var prop in props)
            {
                var propValue = prop.GetValue(entity);

                // 1. Kiểm tra [NotEmpty]
                var notEmptyAttr = prop.GetCustomAttribute<NotEmptyAttribute>();
                if (notEmptyAttr != null)
                {
                    if (propValue == null || string.IsNullOrEmpty(propValue.ToString()))
                    {
                        throw new ValidationException(notEmptyAttr.ErrorMessage);
                    }
                }

                // 2. Kiểm tra [MaxLength] (MỚI)
                var maxLengthAttr = prop.GetCustomAttribute<MaxLengthAttribute>();
                if (maxLengthAttr != null && propValue != null)
                {
                    if (propValue.ToString().Length > maxLengthAttr.Length)
                    {
                        throw new ValidationException(maxLengthAttr.ErrorMessage);
                    }
                }
            }
        }

    }
}

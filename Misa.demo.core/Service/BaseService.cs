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

        /// <summary>
        /// Lấy tất ca theo DTO
        /// </summary>
        /// <returns>danh sách các bản ghi</returns>
        public IEnumerable<T> GetAll()
        {
            return _baseRepo.GetAll();
        }

        /// <summary>
        /// Lấy ca theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Ca</returns>
        /// <exception cref="NotFoundException"></exception>
        public T Get(Guid id)
        {
            var entity= _baseRepo.Get(id);
            if(entity == null)
            {
                throw new NotFoundException($"Không tìm thấy tài nguyên có id <{id}>");
            }
            return entity;
        }

        /// <summary>
        /// Lấy theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>id</returns>
        public T GetById(Guid id)
        {
            return Get(id);
        }

        /// <summary>
        /// Xử lý thêm mới bản ghi
        /// </summary>
        /// <param name="entity">Dữ liệu cần thêm</param>
        /// <returns>Số bản ghi bị ảnh hưởng trong database</returns>
        public int Insert(T entity)
        {
            Validate(entity, "Insert");
           return _baseRepo.Insert(entity);
        }

        /// <summary>
        ///  Xử lý cập nhật bản ghi
        /// </summary>
        /// <param name="entity">Dữ liệu thay đổi</param>
        /// <param name="id">Id của bản ghi cần thay đổi</param>
        /// <returns></returns>
        public int Update(T entity, Guid id)
        {
            Get(id);

            Validate(entity, "Update");
            if(entity.GetType().GetProperty("Id") == null)
            {
               throw new NotFoundException("Đối tượng không có thuộc tính Id");
            }
            return _baseRepo.Update(entity, id);
        }

        /// <summary>
        /// Xử lý xóa bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi cần xóa</param>
        /// <returns>Số bản ghi bị ảnh hưởng trong database</returns>
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

        /// <summary>
        ///  Xử lý xóa nhiều bản ghi
        /// </summary>
        /// <param name="ids">Danh sách id bản ghi cần xóa</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// <exception cref="ValidationException"></exception>
        public int DeleteMany(List<Guid> ids)
        {
            if(ids == null || ids.Count == 0)
            {
                throw new ValidationException("Danh sách id xóa không được rỗng");
            }
            return _baseRepo.DeleteMany(ids);
        }
    }
}

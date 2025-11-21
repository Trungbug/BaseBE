using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.demo.core.Interface.Service
{
    /// <summary>
    /// Interface base service cho tất cả các entity.
    /// </summary>
    /// <typeparam name="T">Loại entity</typeparam>
    public interface IBaseService<T>
    {
        /// <summary>
        /// Lấy tất cả bản ghi cuả entity
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Lấy bản ghi theo Id
        /// </summary>
        /// <param name="id">Id bản ghi cần lấy</param>
        /// <returns>Bản ghi</returns>
        T Get(Guid id);

        /// <summary>
        /// Lấy bản ghi theo Id
        /// </summary>
        /// <param name="id">Id bản ghi cần lấy</param>
        /// <returns>Bản ghi</returns>
        T GetById(Guid id);

        /// <summary>
        /// Xử lý thêm mới bản ghi
        /// </summary>
        /// <param name="entity">Dữ liệu cần thêm</param>
        /// <returns>Số bản ghi bị ảnh hưởng trong database (1 - Thành công, 0 - Thất bại)</returns>
        int Insert(T entity);

        /// <summary>
        /// Xử lý cập nhật bản ghi
        /// </summary>
        /// <param name="entity">Dữ liệu thay đổi</param>
        /// <param name="entityId">Id của bản ghi cần thay đổi</param>
        /// <returns>Số bản ghi bị ảnh hưởng trong database (1 - Thành công, 0 - Thất bại)</returns>
        int Update(T entity, Guid id);

        /// <summary>
        /// Xử lý xóa bản ghi
        /// </summary>
        /// <param name="entityId">Id của bản ghi cần xóa</param>
        /// <returns>Số bản ghi bị ảnh hưởng trong database (1 - Thành công, 0 - Thất bại)</returns>
        int Delete(Guid id);


        /// <summary>
        /// Xử lý xóa nhiều bản ghi
        /// </summary>
        /// <param name="entityId">Id của các bản ghi cần xóa</param>
        /// <returns>Số bản ghi bị ảnh hưởng trong database (> 0 - Thành công, 0 - Thất bại)</returns>
        int DeleteMany(List<Guid> ids);

    }
}

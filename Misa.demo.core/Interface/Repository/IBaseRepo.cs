using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.demo.core.Interface.Repository
{
    public interface IBaseRepo<T>
    {
        /// <summary>
        /// lấy tất cả bảng ghi
        /// </summary>
        /// <returns>danh sách tất cả bản ghi</returns>
        IEnumerable<T> GetAll();


        /// <summary>
        /// lấy bản ghi theo id 
        /// </summary>
        /// <param name="id">Bản ghi</param>
        /// <returns>bản ghi theo id</returns>
        T Get(Guid id);


        /// <summary>
        /// thêm mới bản ghi
        /// </summary>
        /// <param name="enity">Dữ liệu cần thêm</param>
        /// <returns>số bản ghi ảnh hường</returns>
        int Insert(T enity);



        /// <summary>
        /// cập nhật bản ghi
        /// </summary>
        /// <param name="enity">Dữ liệu cần sửa</param>
        /// <param name="id">Id dữ liệu cần sửa</param>
        /// <returns>số bản ghi ảnh hường</returns>
        int Update(T enity, Guid id);

        /// <summary>
        /// xóa 1 bản ghi
        /// </summary>
        /// <param name="id">Id dữ liệu cần xóa</param>
        /// <returns>số bản ghi ảnh hường</returns>
        int Delete(Guid id);

        /// <summary>
        /// xóa nhiều bản ghi
        /// </summary>
        /// <param name="ids">Danh sách Id dữ liệu cần xóa</param>
        /// <returns>Số bản ghi đã xóa</returns>
        int DeleteMany(List<Guid> ids);
    }
}

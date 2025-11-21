using Misa.demo.core.DTOs;
using Misa.demo.core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.demo.core.Interface.Service
{
    public interface IShiftService : IBaseService<Shift>
    {
        /// <summary>
        /// Danh sách phân trang ca làm việc
        /// </summary>
        /// <param name="pageSize">số ca 1 trang</param>
        /// <param name="pageNumber">trang hiện tại </param>
        /// <param name="search">ký tự tìm kiếm</param>
        /// <returns>kết quả trang trả về</returns>
        PagedResult<ShiftDto> GetPaged(int pageSize, int pageNumber, string? search);

        /// <summary>
        /// cập nhật nhiều trạng thái ca làm việc
        /// </summary>
        /// <param name="ids">Danh sách ID ca làm việc</param>
        /// <param name="status">trạng thái cần sửa thành</param>
        /// <returns>số bản ghi cần sửa</returns>
        int UpdateMultipleStatus(List<Guid> ids, int status);

        /// <summary>
        /// xuât dữ liệu excel
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        byte[] ExportExcel(string? search);
    }
}

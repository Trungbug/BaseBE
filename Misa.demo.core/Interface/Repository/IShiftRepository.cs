using Misa.demo.core.DTOs;
using Misa.demo.core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.demo.core.Interface.Repository
{
    public interface IShiftRepository: IBaseRepo<Shift>
    {
        /// <summary>
        /// Hàm lấy tất cả ca theo DTO và phân trang
        /// </summary>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="pageNumber">Trang hiện tại</param>
        /// <param name="search">Từ khóa tìm kiếm</param>
        /// <returns>Tất cả ca theo tìm kiếm và phân trang</returns>
        PagedResult<ShiftDto> GetPaged(int pageSize, int pageNumber, string? search, List<FilterCondition>? filters);        /// <summary>
                                                                                                                             /// Hàm lấy sửa trạng thái theo DTO
                                                                                                                             /// </summary>
                                                                                                                             /// <param name="ids">Danh sách ID ca cần sửa</param>
                                                                                                                             /// <param name="status">trạng thái cần sửa thành</param>
                                                                                                                             /// <returns>thông báo</returns>
        int UpdateMultipleStatus(List<Guid> ids, int status);

        /// <summary>
        /// xuất dữ liệu
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        IEnumerable<ShiftDto> GetExportData(string? search);
    }

}

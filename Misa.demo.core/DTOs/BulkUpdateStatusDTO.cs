using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.demo.core.DTOs
{
    /// <summary>
    /// Dữ liệu dùng để cập nhật trạng thái nhiều bản ghi
    /// </summary>
    public class BulkUpdateStatusDTO
    {
        /// <summary>
        /// Danh sách ID của các bản ghi 
        /// </summary>
        public List<Guid> Ids { get; set; }


        /// <summary>
        /// Trạng thái mới cần cập nhật
        /// </summary>
        public int Status { get; set; }
    }
}

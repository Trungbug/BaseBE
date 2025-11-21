using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.demo.core.DTOs
{
    /// <summary>
    /// Dữ liệu dùng để xóa nhiều bản ghi
    /// </summary>
    public class DeleteManyDTO
    {
        /// <summary>
        /// Danh sách ID của các bản ghi
        /// </summary>
        public List<Guid> Ids { get; set; }
    }
}

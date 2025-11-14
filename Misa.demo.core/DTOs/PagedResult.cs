using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.demo.core.DTOs
{
    public class PagedResult<T>
    {
        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        public int TotalRecords { get; set; }

        /// <summary>
        /// Dữ liệu (danh sách)
        /// </summary>
        public IEnumerable<T> Data { get; set; }
    }
}

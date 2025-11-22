using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.demo.core.DTOs
{
    public class FilterCondition
    {
        /// <summary>
        /// // Tên cột (vd: ShiftCode)
        /// </summary>
        public string Column { get; set; }

        /// <summary>
        /// // Toán tử (Contain, NotContain, StartWith, EndWith)
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// // Giá trị lọc
        /// </summary>
        public string Value { get; set; } 
    }
}

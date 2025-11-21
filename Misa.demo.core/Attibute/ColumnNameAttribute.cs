using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.demo.core.Attibute
{
    /// <summary>
    /// Hiển thị tên cột trong database tương ứng với thuộc tính của class entity
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnNameAttribute : Attribute
    {
        /// <summary>
        /// Tên cột trong database
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// hàm tạo
        /// </summary>
        /// <param name="name">tên cột</param>
        public ColumnNameAttribute(string name)
        {
            Name = name;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.demo.core.Attibute
{
    /// <summary>
    /// Bảng trong database tương ứng với class entity
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : Attribute
    {
        /// <summary>
        /// Tên bảng trong database
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Hàm tạo TableAttribute
        /// </summary>
        /// <param name="tableName"></param>
        public TableAttribute(string tableName)
        {
            TableName = tableName;
        }
    }
}

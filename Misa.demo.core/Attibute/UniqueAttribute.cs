using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.demo.core.Attibute
{
    /// <summary>
    /// Thuộc tính kiểm tra tính duy nhất
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class UniqueAttribute : Attribute
    {
        /// <summary>
        /// Thông báo lỗi
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Hàm tạo UniqueAttribute
        /// </summary>
        /// <param name="errorMessage"></param>
        public UniqueAttribute(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}

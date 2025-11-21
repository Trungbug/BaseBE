using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.demo.core.Attibute
{
    /// <summary>
    ///KHông được để trống
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NotEmptyAttribute : Attribute
    {
        /// <summary>
        /// Thông báo lỗi
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Hàm tạo NotEmptyAttribute
        /// </summary>
        /// <param name="errorMessage"></param>
        public NotEmptyAttribute(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}

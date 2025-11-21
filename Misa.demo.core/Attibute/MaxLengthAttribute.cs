using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.demo.core.Attibute
{
    /// <summary>
    /// attribute kiểm tra độ dài chuỗi
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MaxLengthAttribute : Attribute
    {
        /// <summary>
        /// Độ dài tối đa
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Lỗi trả về khi vượt quá độ dài
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Hàm tạo MaxLengthAttribute
        /// </summary>
        /// <param name="length"></param>
        /// <param name="errorMessage"></param>
        public MaxLengthAttribute(int length, string errorMessage)
        {
            Length = length;
            ErrorMessage = errorMessage;
        }
    }
}

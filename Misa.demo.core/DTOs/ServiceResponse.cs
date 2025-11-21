using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.demo.core.DTOs
{
    /// <summary>
    /// Kết quả trả về từ API
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServiceResponse<T>
    {
        /// <summary>
        /// Trạng thái
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Thông báo cho người dùng
        /// </summary>
        public string Message {get; set; }

        /// <summary>
        /// Dữ liệu trả về
        /// </summary>
        public T? Data{ get; set; }

        /// <summary>
        /// Tạo Response thành công
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <returns>trả thành công</returns>
        public static ServiceResponse<T> Ok(T? data, string message = "Thành công")
        {
            return new ServiceResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        /// <summary>
        ///Response lỗi 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns>Response lỗi </returns>
        public static ServiceResponse<T> Error(string message, T data = default)
        {
            return new ServiceResponse<T>
            {
                Success = false,
                Message = message,
                Data = data
            };
        }
    }
}

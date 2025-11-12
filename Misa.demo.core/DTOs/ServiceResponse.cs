using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.demo.core.DTOs
{
    public class ServiceResponse<T>
    {
        public bool Success { get; set; }

        public string Message {get; set; }

        public T? Data{ get; set; }
        public static ServiceResponse<T> Ok(T? data, string message = "Thành công")
        {
            return new ServiceResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }
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

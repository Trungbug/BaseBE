using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.demo.core.Attibute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MaxLengthAttribute : Attribute
    {
        public int Length { get; set; }
        public string ErrorMessage { get; set; }
        public MaxLengthAttribute(int length, string errorMessage)
        {
            Length = length;
            ErrorMessage = errorMessage;
        }
    }
}

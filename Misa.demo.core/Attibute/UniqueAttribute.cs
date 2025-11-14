using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.demo.core.Attibute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class UniqueAttribute : Attribute
    {
        public string ErrorMessage { get; set; }
        public UniqueAttribute(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}

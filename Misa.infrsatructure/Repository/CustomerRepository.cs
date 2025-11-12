using Microsoft.Extensions.Configuration;
using Misa.demo.core.Entity;
using Misa.demo.core.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.infrsatructure.Repository
{
    internal class CustomerRepository: BaseRepo<Customer>, ICustomerRepository
    {
        public CustomerRepository(IConfiguration config) : base(config) { }
    }
}

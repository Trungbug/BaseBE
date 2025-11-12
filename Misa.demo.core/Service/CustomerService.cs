using Misa.demo.core.Entity;
using Misa.demo.core.Interface.Repository;
using Misa.demo.core.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.demo.core.Service
{
    public class CustomerService : BaseService<Customer>, ICustomerService
    {
        
       private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository) : base(customerRepository)
        {
            _customerRepository = customerRepository;
        }
    }
}

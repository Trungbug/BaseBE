using Microsoft.AspNetCore.Mvc;
using Misa.demo.core.Entity;
using Misa.demo.core.Interface.Service;

namespace Misa_FS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : MSBaseController<Customer>
    {
        public CustomerController(IBaseService<Customer> baseService) : base(baseService)
        {

        }
    }
}

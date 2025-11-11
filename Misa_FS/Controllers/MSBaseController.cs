
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa.demo.core.Interface.Service;


namespace Misa_FS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MSBaseController<T> : ControllerBase
    {
        IBaseService<T> _baseService;


        public MSBaseController(IBaseService<T> baseService)
        {
            _baseService = baseService;
        }

        [HttpPost]
        public IActionResult Post(T entity)
        {
            var res = _baseService.Insert(entity);
            return Ok(res);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = _baseService.GetAll();
            return Ok(data);
        }

        [HttpPut]
        public IActionResult Put(T entity, [FromRoute] Guid id) 
        {

            var res = _baseService.Update(entity, id);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var res = _baseService.Delete(id);
            return Ok(res);
        }


    }
}
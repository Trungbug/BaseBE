using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa.demo.core.Interface.Service;
using Misa.demo.core.DTOs;
using static Misa.demo.core.DTOs.ServiceResponse<int>;

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
            return StatusCode(201, ServiceResponse<int>.Ok(res, "Thêm mới thành công"));
        }

        //[HttpGet]
        //public IActionResult Get()
        //{
        //    var data = _baseService.GetAll();
        //    return Ok(ServiceResponse<IEnumerable<T>>.Ok(data, "Lấy dữ liệu thành công"));
        //}

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] T entity, [FromRoute] Guid id) 
        {
            var res = _baseService.Update(entity, id);
            return Ok(ServiceResponse<int>.Ok(res, "Cập nhật thành công"));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var res = _baseService.Delete(id);
            return Ok(ServiceResponse<int>.Ok(res, "Xóa thành công"));
        }
    }
}
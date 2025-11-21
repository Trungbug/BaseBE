using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa.demo.core.DTOs;
using Misa.demo.core.Interface.Service;
using Misa.demo.core.Service;
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

        /// <summary>
        /// Xử lý thêm mới bản ghi
        /// </summary>
        /// <param name="entity">Dữ liệu cần thêm</param>
        /// <returns>Số bản ghi bị ảnh hưởng trong database </returns>
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



        /// <summary>
        /// xử lý cập nhật bản ghi
        /// </summary>
        /// <param name="entity">Dữ liệu thay đổi</param>
        /// <param name="id">Id của bản ghi cần thay đổi</param>
        /// <returns>Số bản ghi bị ảnh hưởng trong database </returns>
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] T entity, [FromRoute] Guid id) 
        {
            var res = _baseService.Update(entity, id);
            return Ok(ServiceResponse<int>.Ok(res, "Cập nhật thành công"));
        }

        /// <summary>
        /// API xóa 1 bản ghi (Delete)
        /// </summary>
        /// <param name="id">ID bản ghi cần xóa</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var res = _baseService.Delete(id);
            return Ok(ServiceResponse<int>.Ok(res, "Xóa thành công"));
        }

        /// <summary>
        /// API xóa nhiều bản ghi cùng lúc
        /// </summary>
        /// <param name="dto">Danh sách ID cần xóa</param>
        /// <returns>Số lượng bản ghi đã xóa</returns>
        [HttpDelete("delete-many")]
        public IActionResult DeleteMany([FromBody] DeleteManyDTO dto)
        {
            if (dto == null || dto.Ids == null || dto.Ids.Count == 0)
            {
                return BadRequest(ServiceResponse<object>.Error("Danh sách id xóa không được để trống"));
            }

            var res = _baseService.DeleteMany(dto.Ids);
            return Ok(ServiceResponse<int>.Ok(res, $"Đã xóa thành công {res} bản ghi"));
        }
    }
}